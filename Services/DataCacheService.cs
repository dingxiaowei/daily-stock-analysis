using System.Collections.Generic;
using System.Linq;
using StockDatasCollection.Models;

namespace StockDatasCollection.Services
{
    /// <summary>
    /// Thread-safe in-memory cache of collected StockDataPoint records.
    /// Key = stock code (e.g. "sh600519").
    /// </summary>
    public class DataCacheService
    {
        private readonly Dictionary<string, List<StockDataPoint>> _cache
            = new Dictionary<string, List<StockDataPoint>>();
        private readonly object _lock = new object();

        public void Add(StockDataPoint point)
        {
            lock (_lock)
            {
                if (!_cache.ContainsKey(point.StockCode))
                    _cache[point.StockCode] = new List<StockDataPoint>();
                _cache[point.StockCode].Add(point);
            }
        }

        public void AddRange(IEnumerable<StockDataPoint> points)
        {
            lock (_lock)
            {
                foreach (var p in points)
                {
                    if (!_cache.ContainsKey(p.StockCode))
                        _cache[p.StockCode] = new List<StockDataPoint>();
                    _cache[p.StockCode].Add(p);
                }
            }
        }

        /// <summary>Returns a snapshot of all cached data points across all stocks.</summary>
        public List<StockDataPoint> GetAll()
        {
            lock (_lock)
            {
                return _cache.Values.SelectMany(l => l).ToList();
            }
        }

        /// <summary>Returns all data points for a specific stock code.</summary>
        public List<StockDataPoint> GetByCode(string code)
        {
            lock (_lock)
            {
                if (_cache.TryGetValue(code, out List<StockDataPoint> list))
                    return new List<StockDataPoint>(list);
                return new List<StockDataPoint>();
            }
        }

        /// <summary>Returns the latest data point for each stock code.</summary>
        public List<StockDataPoint> GetLatestPerStock()
        {
            lock (_lock)
            {
                var result = new List<StockDataPoint>();
                foreach (var list in _cache.Values)
                    if (list.Count > 0)
                        result.Add(list[list.Count - 1]);
                return result;
            }
        }

        /// <summary>Returns snapshot for archiving (grouped by stock code).</summary>
        public Dictionary<string, List<StockDataPoint>> GetSnapshot()
        {
            lock (_lock)
            {
                var snapshot = new Dictionary<string, List<StockDataPoint>>();
                foreach (var kv in _cache)
                    snapshot[kv.Key] = new List<StockDataPoint>(kv.Value);
                return snapshot;
            }
        }

        public int GetTotalCount()
        {
            lock (_lock)
            {
                int total = 0;
                foreach (var list in _cache.Values) total += list.Count;
                return total;
            }
        }

        public void Clear()
        {
            lock (_lock) { _cache.Clear(); }
        }
    }
}
