using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Timers;
using System.Web;
using System.Windows.Forms;
using StockDatasCollection.IO;
using StockDatasCollection.Models;
using StockDatasCollection.Services;

namespace StockDatasCollection.Forms
{
    public partial class MainForm : Form
    {
        // --- Services ---
        private readonly StockCodeManager _codeManager = new StockCodeManager();
        private readonly StockDataCollector _collector = new StockDataCollector();
        private readonly DataCacheService _cache = new DataCacheService();
        private readonly ArchiveService _archiver = new ArchiveService();

        // --- 分时K线图：最近一次加载的数据 ---
        private List<StockDataPoint> _loadedPoints = new List<StockDataPoint>();

        // --- Timers ---
        private System.Timers.Timer _collectTimer;
        private bool _collecting;
        private DateTime? _lastCollectTime;
        private DateTime? _nextCollectTime;

        // UI refresh timer (runs on UI thread)
        private System.Windows.Forms.Timer _uiTimer;

        public MainForm()
        {
            InitializeComponent();
            _archiver.ArchiveCompleted += OnArchiveCompleted;
            _uiTimer = new System.Windows.Forms.Timer { Interval = 1000 };
            _uiTimer.Tick += OnUiTimerTick;
            _uiTimer.Start();
        }

        // ============================================================
        // Form Load
        // ============================================================
        private void MainForm_Load(object sender, EventArgs e)
        {
            RefreshStockGrid();
            // Set default archive directory
            txtArchiveDir.Text = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Archives");
            RefreshChartHtml();
            // 存档时段限制与“仅开盘时段采集”开关联动
            _archiver.EnforceTradingHours = chkTimeRestrict.Checked;
            chkTimeRestrict.CheckedChanged += (s, ev) => { _archiver.EnforceTradingHours = chkTimeRestrict.Checked; };
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _collectTimer?.Stop();
            _archiver.Stop();
            _uiTimer.Stop();
        }

        // ============================================================
        // Tab 1 — 股票管理
        // ============================================================
        private void RefreshStockGrid()
        {
            var stocks = _codeManager.GetAll();
            dgvStocks.Rows.Clear();
            for (int i = 0; i < stocks.Count; i++)
            {
                var s = stocks[i];
                dgvStocks.Rows.Add(i + 1, s.Code, s.Name, s.Notes);
            }
        }

        private void btnAddStock_Click(object sender, EventArgs e)
        {
            using (var form = new AddEditStockForm())
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        _codeManager.Add(form.Result);
                        RefreshStockGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "添加失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnEditStock_Click(object sender, EventArgs e)
        {
            if (dgvStocks.CurrentRow == null) return;
            string code = dgvStocks.CurrentRow.Cells["colCode"].Value?.ToString();
            var existing = _codeManager.GetAll().Find(s => s.Code == code);
            if (existing == null) return;

            using (var form = new AddEditStockForm(existing))
            {
                if (form.ShowDialog(this) == DialogResult.OK)
                {
                    try
                    {
                        var updated = new StockCode
                        {
                            Code = existing.Code,
                            Name = form.Result.Name,
                            Notes = form.Result.Notes
                        };
                        _codeManager.Update(updated);
                        RefreshStockGrid();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "编辑失败", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
            }
        }

        private void btnDeleteStock_Click(object sender, EventArgs e)
        {
            if (dgvStocks.CurrentRow == null) return;
            string code = dgvStocks.CurrentRow.Cells["colCode"].Value?.ToString();
            if (string.IsNullOrEmpty(code)) return;

            var answer = MessageBox.Show($"确认删除股票 {code}？",
                "确认删除", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (answer == DialogResult.Yes)
            {
                _codeManager.Remove(code);
                RefreshStockGrid();
            }
        }

        // ============================================================
        // Tab 2 — 数据采集
        // ============================================================
        /// <summary>
        /// 判断当前时间是否在采集有效时段（含 2 分钟缓冲）：上午 9:13-11:32、下午 12:58-15:02。
        /// 仅在此时段内才实际发起采集。
        /// </summary>
        private static bool IsInTradingHours()
        {
            var t = DateTime.Now.TimeOfDay;
            var morningStart = new TimeSpan(9, 13, 0);
            var morningEnd = new TimeSpan(11, 32, 0);
            var afternoonStart = new TimeSpan(12, 58, 0);
            var afternoonEnd = new TimeSpan(15, 2, 0);
            return (t >= morningStart && t <= morningEnd) || (t >= afternoonStart && t <= afternoonEnd);
        }

        private void btnStartCollect_Click(object sender, EventArgs e)
        {
            var codes = _codeManager.GetAll();
            if (codes.Count == 0)
            {
                MessageBox.Show("请先添加关注的股票代码。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            int interval = (int)nudCollectInterval.Value;
            _collectTimer?.Stop();
            _collectTimer = new System.Timers.Timer(interval * 1000.0);
            _collectTimer.Elapsed += (s, ev) => DoCollect();
            _collectTimer.AutoReset = true;
            _collectTimer.Start();

            _nextCollectTime = DateTime.Now.AddSeconds(interval);
            lblCollectStatus.Text = "状态：采集中";
            btnStartCollect.Enabled = false;
            btnStopCollect.Enabled = true;

            // Trigger one immediate collection
            DoCollect();
        }

        private void btnStopCollect_Click(object sender, EventArgs e)
        {
            _collectTimer?.Stop();
            _collectTimer = null;
            _nextCollectTime = null;
            lblCollectStatus.Text = "状态：已停止";
            btnStartCollect.Enabled = true;
            btnStopCollect.Enabled = false;
        }

        private async void DoCollect()
        {
            if (_collecting) return;
            _collecting = true;
            try
            {
                var codes = _codeManager.GetAll();
                if (codes.Count == 0) return;

                // 若勾选“仅开盘时段采集”，则非开盘时段不采集，方便测试时可关闭此限制
                if (chkTimeRestrict.Checked && !IsInTradingHours()) return;

                var points = await _collector.FetchAsync(codes);
                _cache.AddRangeDedupeByMinute(points);
                _lastCollectTime = DateTime.Now;
                _nextCollectTime = DateTime.Now.AddSeconds((int)nudCollectInterval.Value);

                // Auto-update stock names
                foreach (var p in points)
                    _codeManager.UpdateName(p.StockCode, p.StockName);
            }
            catch (Exception ex)
            {
                // Log collection error without crashing
                AppendArchiveLog($"[{DateTime.Now:HH:mm:ss}] 采集错误: {ex.Message}");
            }
            finally
            {
                _collecting = false;
            }
        }

        // ============================================================
        // Tab 3 — 数据存档
        // ============================================================
        private void btnBrowseArchiveDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = txtArchiveDir.Text;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtArchiveDir.Text = dlg.SelectedPath;
            }
        }

        private void btnStartArchive_Click(object sender, EventArgs e)
        {
            _archiver.ArchiveIntervalSeconds = (int)nudArchiveInterval.Value;
            _archiver.ArchiveDirectory = txtArchiveDir.Text;
            _archiver.Start(_cache);
            btnStartArchive.Enabled = false;
            btnStopArchive.Enabled = true;
            AppendArchiveLog($"[{DateTime.Now:HH:mm:ss}] 存档已启动，间隔 {_archiver.ArchiveIntervalSeconds} 秒。");
        }

        private void btnStopArchive_Click(object sender, EventArgs e)
        {
            _archiver.Stop();
            btnStartArchive.Enabled = true;
            btnStopArchive.Enabled = false;
            AppendArchiveLog($"[{DateTime.Now:HH:mm:ss}] 存档已停止。");
        }

        private void btnArchiveNow_Click(object sender, EventArgs e)
        {
            if (chkTimeRestrict.Checked && !IsInTradingHours())
            {
                MessageBox.Show("当前不在开盘时段，存档仅在有效时段内生效。\n有效时段：9:13-11:32、12:58-15:02。\n（取消勾选「仅开盘时段采集」可随时存档测试）",
                    "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            _archiver.ArchiveDirectory = txtArchiveDir.Text;
            try
            {
                _archiver.ArchiveNow(_cache);
            }
            catch (Exception ex)
            {
                AppendArchiveLog($"[{DateTime.Now:HH:mm:ss}] 手动存档失败: {ex.Message}");
            }
        }

        private void OnArchiveCompleted(object sender, string message)
        {
            // Marshal to UI thread
            if (InvokeRequired)
                BeginInvoke(new Action(() => AppendArchiveLog(message)));
            else
                AppendArchiveLog(message);
        }

        private void AppendArchiveLog(string msg)
        {
            if (InvokeRequired) { BeginInvoke(new Action(() => AppendArchiveLog(msg))); return; }
            rtbArchiveLog.AppendText(msg + Environment.NewLine);
            rtbArchiveLog.ScrollToCaret();
        }

        // ============================================================
        // Tab 4 — 数据加载
        // ============================================================
        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            using (var dlg = new OpenFileDialog())
            {
                dlg.Filter = "Archive files (*.bin)|*.bin|All files (*.*)|*.*";
                dlg.InitialDirectory = txtLoadPath.Text.Length > 0
                    ? Path.GetDirectoryName(txtLoadPath.Text)
                    : _archiver.ArchiveDirectory;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtLoadPath.Text = dlg.FileName;
            }
        }

        private void btnBrowseDir_Click(object sender, EventArgs e)
        {
            using (var dlg = new FolderBrowserDialog())
            {
                dlg.SelectedPath = txtArchiveDir.Text;
                if (dlg.ShowDialog(this) == DialogResult.OK)
                    txtLoadPath.Text = dlg.SelectedPath;
            }
        }

        private void btnLoad_Click(object sender, EventArgs e)
        {
            string path = txtLoadPath.Text.Trim();
            if (string.IsNullOrEmpty(path))
            {
                MessageBox.Show("请先选择文件或目录。", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            var loader = new ArchiveDataLoader();
            List<StockDataPoint> points;
            List<string> errors = new List<string>();

            try
            {
                if (File.Exists(path))
                {
                    points = loader.Load(path);
                }
                else if (Directory.Exists(path))
                {
                    var result = loader.LoadDirectory(path);
                    points = result.Item1;
                    errors = result.Item2;
                }
                else
                {
                    MessageBox.Show("路径不存在。", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"加载失败：{ex.Message}", "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            PopulateLoadGrid(points);
            _loadedPoints = points;
            RefreshChartHtml();
            string summary = $"共加载 {points.Count} 条记录。";
            if (errors.Count > 0) summary += $" {errors.Count} 个文件读取失败。";
            lblLoadStatus.Text = summary;

            if (errors.Count > 0)
                MessageBox.Show("以下文件读取失败：\n" + string.Join("\n", errors),
                    "部分错误", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void PopulateLoadGrid(List<StockDataPoint> points)
        {
            dgvLoadData.Rows.Clear();
            foreach (var p in points)
            {
                dgvLoadData.Rows.Add(
                    p.StockCode, p.StockName,
                    p.CollectedAt.ToString("yyyy-MM-dd HH:mm:ss"),
                    p.TradeDate, p.TradeTime,
                    p.CurrentPrice, p.OpenPrice, p.PreClosePrice,
                    p.HighPrice, p.LowPrice,
                    p.Volume, p.Turnover,
                    p.Buy1Vol, p.Buy1Price,
                    p.Sell1Vol, p.Sell1Price,
                    p.StatusCode);
            }
        }

        // ============================================================
        // UI Refresh Timer (1 second tick)
        // ============================================================
        private void OnUiTimerTick(object sender, EventArgs e)
        {
            // Tab 2: update status labels
            if (_lastCollectTime.HasValue)
                lblLastCollect.Text = "最后采集：" + _lastCollectTime.Value.ToString("HH:mm:ss");
            if (_nextCollectTime.HasValue)
                lblNextCollect.Text = "下次采集：" + _nextCollectTime.Value.ToString("HH:mm:ss");

            lblCacheCount.Text = "缓存记录：" + _cache.GetTotalCount();

            // Refresh live data grid on Tab 2
            RefreshLiveDataGrid();

            // Tab 3: archive status
            if (_archiver.LastArchiveTime.HasValue)
                lblLastArchive.Text = "最后存档：" + _archiver.LastArchiveTime.Value.ToString("HH:mm:ss");
            if (_archiver.NextArchiveTime.HasValue)
                lblNextArchive.Text = "下次存档：" + _archiver.NextArchiveTime.Value.ToString("HH:mm:ss");
        }

        private void RefreshLiveDataGrid()
        {
            var latest = _cache.GetLatestPerStock();
            dgvLiveData.Rows.Clear();
            foreach (var p in latest)
            {
                dgvLiveData.Rows.Add(
                    p.StockCode, p.StockName,
                    p.CurrentPrice, p.OpenPrice, p.PreClosePrice,
                    p.HighPrice, p.LowPrice,
                    p.Volume, p.Turnover,
                    p.TradeDate, p.TradeTime);
            }
        }

        // ============================================================
        // Tab 5 — 分时K线图（WebBrowser）
        // ============================================================
        private void btnRefreshChart_Click(object sender, EventArgs e)
        {
            RefreshChartHtml();
        }

        private void RefreshChartHtml()
        {
            if (InvokeRequired) { BeginInvoke(new Action(RefreshChartHtml)); return; }

            if (_loadedPoints == null || _loadedPoints.Count == 0)
            {
                lblChartHint.Text = "暂无数据，请在「数据加载」页加载分时数据后刷新。";
                webBrowserChart.DocumentText = BuildEmptyChartHtml();
                return;
            }

            // 按股票分组，取数据量最多的一只用于绘图
            var byStock = _loadedPoints
                .GroupBy(p => p.StockCode)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault();
            if (byStock == null) return;

            var points = byStock.OrderBy(p => p.TradeDate).ThenBy(p => p.TradeTime).ToList();
            string title = $"{points[0].StockCode} {points[0].StockName} 分时";
            lblChartHint.Text = $"共 {points.Count} 个分时点 · {title}";

            string html = BuildChartHtml(points, title);
            webBrowserChart.DocumentText = html;
        }

        private static string BuildEmptyChartHtml()
        {
            return @"<!DOCTYPE html><html><head><meta charset='utf-8'><title>分时K线</title></head>
<body style='margin:0;font-family:Microsoft YaHei;background:#1a1a2e;color:#eee;display:flex;align-items:center;justify-content:center;height:100vh;'>
<div>暂无数据，请在「数据加载」页加载分时数据后点击「刷新图表」。</div>
</body></html>";
        }

        private static string BuildChartHtml(List<StockDataPoint> points, string title)
        {
            decimal preClose = 0;
            decimal.TryParse(points[0].PreClosePrice, out preClose);
            if (preClose <= 0) decimal.TryParse(points[0].OpenPrice, out preClose);

            var times = new List<string>();
            var prices = new List<decimal>();
            var volumes = new List<long>();

            foreach (var p in points)
            {
                string t = string.IsNullOrEmpty(p.TradeTime) ? p.TradeDate : (p.TradeDate + " " + p.TradeTime);
                times.Add(t);
                decimal price;
                decimal.TryParse(p.CurrentPrice, out price);
                prices.Add(price);
                volumes.Add(p.Volume);
            }

            var sb = new StringBuilder();
            sb.Append("var times = ");
            sb.Append(ToJsonArray(times));
            sb.Append("; var prices = ");
            sb.Append(ToJsonArray(prices));
            sb.Append("; var volumes = ");
            sb.Append(ToJsonArray(volumes));
            sb.Append("; var preClose = ");
            sb.Append(preClose.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
            sb.Append(";");
            string dataJson = sb.ToString();

            string html = @"<!DOCTYPE html>
<html>
<head>
<meta charset='utf-8'>
<meta http-equiv='X-UA-Compatible' content='IE=Edge'>
<title>分时K线</title>
<script src='https://cdn.jsdelivr.net/npm/echarts@4.9.0/dist/echarts.min.js'></script>
</head>
<body style='margin:0;background:#1a1a2e;font-family:Microsoft YaHei;'>
<div id='main' style='width:100%;height:100vh;'></div>
<script>
" + dataJson + @"
var dom = document.getElementById('main');
var chart = echarts.init(dom);
var option = {
  title: { text: '" + HttpUtility.JavaScriptStringEncode(title) + @"', left: 'center', textStyle: { color: '#e0e0e0', fontSize: 14 } },
  tooltip: { trigger: 'axis' },
  legend: { data: ['价格', '成交量'], top: 28, textStyle: { color: '#aaa' } },
  grid: [ { left: '10%', right: '8%', top: '18%', height: '45%' }, { left: '10%', right: '8%', top: '70%', height: '22%' } ],
  xAxis: [
    { type: 'category', data: times, gridIndex: 0, axisLabel: { color: '#888' }, axisLine: { lineStyle: { color: '#444' } } },
    { type: 'category', data: times, gridIndex: 1, axisLabel: { color: '#888' }, axisLine: { lineStyle: { color: '#444' } } }
  ],
  yAxis: [
    { type: 'value', gridIndex: 0, scale: true, splitLine: { lineStyle: { color: '#333' } }, axisLabel: { color: '#888' } },
    { type: 'value', gridIndex: 1, splitLine: { show: false }, axisLabel: { color: '#888' } }
  ],
  dataZoom: [ { type: 'inside', xAxisIndex: [0, 1], start: 0, end: 100 }, { type: 'slider', xAxisIndex: [0, 1], start: 0, end: 100, bottom: 8, height: 20 } ],
  series: [
    { name: '价格', type: 'line', data: prices, xAxisIndex: 0, yAxisIndex: 0, smooth: false, symbol: 'none', lineStyle: { color: '#00d4aa', width: 2 }, markLine: { silent: true, data: [{ yAxis: preClose, lineStyle: { color: '#666', type: 'dashed' }, label: { formatter: '昨收 ' + preClose, color: '#888' } }] } },
    { name: '成交量', type: 'bar', data: volumes, xAxisIndex: 1, yAxisIndex: 1, itemStyle: { color: function(params) { return prices[params.dataIndex] >= preClose ? '#e74c3c' : '#00d4aa'; } } }
  ]
};
chart.setOption(option);
window.onresize = function() { chart.resize(); };
</script>
</body>
</html>";
            return html;
        }

        private static string ToJsonArray(IEnumerable<string> list)
        {
            var sb = new StringBuilder("[");
            bool first = true;
            foreach (var s in list)
            {
                if (!first) sb.Append(",");
                sb.Append("\"").Append(HttpUtility.JavaScriptStringEncode(s ?? "")).Append("\"");
                first = false;
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string ToJsonArray(IEnumerable<decimal> list)
        {
            var sb = new StringBuilder("[");
            bool first = true;
            foreach (var v in list)
            {
                if (!first) sb.Append(",");
                sb.Append(v.ToString("G", System.Globalization.CultureInfo.InvariantCulture));
                first = false;
            }
            sb.Append("]");
            return sb.ToString();
        }

        private static string ToJsonArray(IEnumerable<long> list)
        {
            var sb = new StringBuilder("[");
            bool first = true;
            foreach (var v in list)
            {
                if (!first) sb.Append(",");
                sb.Append(v);
                first = false;
            }
            sb.Append("]");
            return sb.ToString();
        }
    }
}
