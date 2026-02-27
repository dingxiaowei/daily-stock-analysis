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
    /// Archive path: {ArchiveDirectory}/{StockName}/{TradeDate}/{HH-mm-ss}.bin
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

        private void TryArchive(DataCacheService cache)
        {
            if (_archiving) return;
            _archiving = true;
            try
            {
                ArchiveNow(cache);
            }
            finally
            {
                _archiving = false;
                NextArchiveTime = DateTime.Now.AddSeconds(ArchiveIntervalSeconds);
            }
        }

        /// <summary>Immediately archive all cached data, then clear the cache.</summary>
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

            // Group by StockName, then by TradeDate
            foreach (var kv in snapshot)
            {
                string stockCode = kv.Key;
                List<StockDataPoint> points = kv.Value;
                if (points.Count == 0) continue;

                // Group by (StockName, TradeDate)
                var groups = points
                    .GroupBy(p => new { Name = SanitizeName(p.StockName), Date = p.TradeDate });

                foreach (var group in groups)
                {
                    string stockName = string.IsNullOrWhiteSpace(group.Key.Name) ? stockCode : group.Key.Name;
                    string tradeDate = string.IsNullOrWhiteSpace(group.Key.Date)
                        ? DateTime.Now.ToString("yyyy-MM-dd")
                        : group.Key.Date;
                    string timeStamp = DateTime.Now.ToString("HH-mm-ss");

                    string dir = Path.Combine(ArchiveDirectory, stockName, tradeDate);
                    try
                    {
                        Directory.CreateDirectory(dir);
                        string filePath = Path.Combine(dir, timeStamp + ".bin");
                        WriteArchiveFile(filePath, stockCode, group.ToList());
                        totalWritten += group.Count();
                    }
                    catch (Exception ex)
                    {
                        errors.Add($"{stockName}: {ex.Message}");
                    }
                }
            }

            cache.Clear();
            LastArchiveTime = DateTime.Now;

            string msg = $"[{DateTime.Now:HH:mm:ss}] 存档完成，共写入 {totalWritten} 条记录。";
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
