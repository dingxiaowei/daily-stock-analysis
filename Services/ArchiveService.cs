using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Timers;
using StockDatasCollection.Models;

namespace StockDatasCollection.Services
{
    /// <summary>
    /// Periodically archives cached stock data to binary files.
    /// Archive path: {ArchiveDirectory}/{StockName}/{TradeDate}/{HH-mm-ss-fff}.bin
    /// 每次触发存档，将“本次间隔内缓存的数据”打包为一个二进制文件。
    /// Binary format: magic "STDA" + version + stock code + record count + records.
    /// </summary>
    public class ArchiveService : IDisposable
    {
        private Timer _timer;
        private volatile bool _archiving;

        public int ArchiveIntervalSeconds { get; set; } = 60;
        public string ArchiveDirectory { get; set; } = "Archives";

        /// <summary>Raised on UI thread by caller after marshal. Arg = log message.</summary>
        public event EventHandler<string> ArchiveCompleted;

        public bool IsRunning { get; private set; }
        public DateTime? LastArchiveTime { get; private set; }
        public DateTime? NextArchiveTime { get; private set; }

        /// <summary>为 true 时仅在开盘时段存档；为 false 时任意时间可存档（与“仅开盘时段采集”开关联动，便于测试）。</summary>
        public bool EnforceTradingHours { get; set; } = true;

        public void Start(DataCacheService cache)
        {
            if (IsRunning) return;
            _timer = new Timer(ArchiveIntervalSeconds * 1000.0);
            _timer.Elapsed += (s, e) => TryArchive(cache);
            _timer.AutoReset = true;
            _timer.Start();
            IsRunning = true;
            NextArchiveTime = DateTime.Now.AddSeconds(ArchiveIntervalSeconds);
        }

        public void Stop()
        {
            _timer?.Stop();
            _timer?.Dispose();
            _timer = null;
            IsRunning = false;
            NextArchiveTime = null;
        }

        /// <summary>
        /// 与主窗体一致的采集/存档有效时段（9:13-11:32、12:58-15:02），仅在此时段内执行存档。
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

        private void TryArchive(DataCacheService cache)
        {
            if (_archiving) return;
            _archiving = true;
            try
            {
                if (EnforceTradingHours && !IsInTradingHours())
                {
                    OnArchiveCompleted("[" + DateTime.Now.ToString("HH:mm:ss") + "] 当前不在开盘时段，跳过本次存档。");
                    return;
                }
                ArchiveNow(cache);
            }
            finally
            {
                _archiving = false;
                NextArchiveTime = DateTime.Now.AddSeconds(ArchiveIntervalSeconds);
            }
        }

        /// <summary>
        /// 按“存档间隔窗口”打包存档：同一股票、同一交易日，本次缓存数据写入一个文件。
        /// 分钟级去重由 DataCacheService 在入缓存时完成，存档阶段不再按分钟拆文件。
        /// </summary>
        public void ArchiveNow(DataCacheService cache)
        {
            var snapshot = cache.GetSnapshot();
            if (snapshot.Count == 0 || snapshot.Values.All(l => l.Count == 0))
            {
                OnArchiveCompleted("[" + DateTime.Now.ToString("HH:mm:ss") + "] 缓存为空，跳过存档。");
                return;
            }

            int totalWritten = 0;
            var errors = new List<string>();

            foreach (var kv in snapshot)
            {
                string stockCode = kv.Key;
                List<StockDataPoint> points = kv.Value;
                if (points.Count == 0) continue;

                // 按 (名称, 交易日期) 分组：本次存档窗口内的数据打包到同一个文件
                var groups = points
                    .GroupBy(p => new
                    {
                        Name = SanitizeName(p.StockName),
                        Date = string.IsNullOrWhiteSpace(p.TradeDate) ? DateTime.Now.ToString("yyyy-MM-dd") : p.TradeDate
                    });

                foreach (var group in groups)
                {
                    string stockName = string.IsNullOrWhiteSpace(group.Key.Name) ? stockCode : group.Key.Name;
                    string tradeDate = group.Key.Date;
                    string fileName = DateTime.Now.ToString("HH-mm-ss-fff") + ".bin";

                    string dir = Path.Combine(ArchiveDirectory, stockName, tradeDate);
                    try
                    {
                        Directory.CreateDirectory(dir);
                        string filePath = Path.Combine(dir, fileName);
                        WriteArchiveFile(filePath, stockCode, group
                            .OrderBy(p => p.TradeDate)
                            .ThenBy(p => p.TradeTime)
                            .ToList());
                        totalWritten += group.Count();
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"{stockName}/{tradeDate}/{fileName}: {ex.Message}");
                    }
                }
            }

            cache.Clear();
            LastArchiveTime = DateTime.Now;

            string msg = $"[{DateTime.Now:HH:mm:ss}] 存档完成（按间隔打包），共写入 {totalWritten} 条记录。";
            if (errors.Count > 0) msg += " 错误: " + string.Join("; ", errors);
            OnArchiveCompleted(msg);
        }

        private void WriteArchiveFile(string filePath, string stockCode, List<StockDataPoint> points)
        {
            using (var fs = new FileStream(filePath, FileMode.Create, FileAccess.Write))
            using (var bw = new BinaryWriter(fs, System.Text.Encoding.UTF8))
            {
                // Header
                bw.Write(new char[] { 'S', 'T', 'D', 'A' });  // magic (4 bytes)
                bw.Write((byte)1);                              // version
                bw.Write(stockCode ?? string.Empty);           // stock code
                bw.Write(points.Count);                        // record count

                foreach (var p in points)
                {
                    bw.Write(p.CollectedAt.Ticks);
                    bw.Write(p.StockName ?? string.Empty);
                    bw.Write(p.OpenPrice ?? string.Empty);
                    bw.Write(p.PreClosePrice ?? string.Empty);
                    bw.Write(p.CurrentPrice ?? string.Empty);
                    bw.Write(p.HighPrice ?? string.Empty);
                    bw.Write(p.LowPrice ?? string.Empty);
                    bw.Write(p.BidPrice ?? string.Empty);
                    bw.Write(p.AskPrice ?? string.Empty);
                    bw.Write(p.Volume);
                    bw.Write(p.Turnover ?? string.Empty);
                    bw.Write(p.Buy1Vol);  bw.Write(p.Buy1Price ?? string.Empty);
                    bw.Write(p.Buy2Vol);  bw.Write(p.Buy2Price ?? string.Empty);
                    bw.Write(p.Buy3Vol);  bw.Write(p.Buy3Price ?? string.Empty);
                    bw.Write(p.Buy4Vol);  bw.Write(p.Buy4Price ?? string.Empty);
                    bw.Write(p.Buy5Vol);  bw.Write(p.Buy5Price ?? string.Empty);
                    bw.Write(p.Sell1Vol); bw.Write(p.Sell1Price ?? string.Empty);
                    bw.Write(p.Sell2Vol); bw.Write(p.Sell2Price ?? string.Empty);
                    bw.Write(p.Sell3Vol); bw.Write(p.Sell3Price ?? string.Empty);
                    bw.Write(p.Sell4Vol); bw.Write(p.Sell4Price ?? string.Empty);
                    bw.Write(p.Sell5Vol); bw.Write(p.Sell5Price ?? string.Empty);
                    bw.Write(p.TradeDate ?? string.Empty);
                    bw.Write(p.TradeTime ?? string.Empty);
                    bw.Write(p.StatusCode ?? string.Empty);
                }
            }
        }

        private static string SanitizeName(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return string.Empty;
            foreach (char c in Path.GetInvalidFileNameChars())
                name = name.Replace(c.ToString(), "_");
            return name.Trim();
        }

        private void OnArchiveCompleted(string message)
        {
            ArchiveCompleted?.Invoke(this, message);
        }

        public void Dispose()
        {
            Stop();
        }
    }
}
