using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using StockDatasCollection.Models;

namespace StockDatasCollection.Services
{
    /// <summary>
    /// Manages the watchlist of stock codes, persisted to stocks.xml.
    /// </summary>
    public class StockCodeManager
    {
        private readonly string _filePath;
        private List<StockCode> _stocks;

        public StockCodeManager(string filePath = null)
        {
            _filePath = filePath ?? Path.Combine(
                AppDomain.CurrentDomain.BaseDirectory, "stocks.xml");
            _stocks = new List<StockCode>();
            Load();
        }

        public List<StockCode> GetAll() => new List<StockCode>(_stocks);

        public void Add(StockCode stock)
        {
            if (stock == null) throw new ArgumentNullException("stock");
            if (string.IsNullOrWhiteSpace(stock.Code))
                throw new ArgumentException("股票代码不能为空");
            if (_stocks.Exists(s => s.Code.Equals(stock.Code, StringComparison.OrdinalIgnoreCase)))
                throw new InvalidOperationException($"代码 {stock.Code} 已存在");
            _stocks.Add(stock);
            Save();
        }

        public void Update(StockCode updated)
        {
            if (updated == null) throw new ArgumentNullException("updated");
            int idx = _stocks.FindIndex(s => s.Code.Equals(updated.Code, StringComparison.OrdinalIgnoreCase));
            if (idx < 0) throw new KeyNotFoundException($"未找到代码 {updated.Code}");
            _stocks[idx] = updated;
            Save();
        }

        public void Remove(string code)
        {
            int removed = _stocks.RemoveAll(s => s.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (removed > 0) Save();
        }

        /// <summary>
        /// Called after a successful fetch to update the stock name automatically.
        /// </summary>
        public void UpdateName(string code, string name)
        {
            var stock = _stocks.Find(s => s.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            if (stock != null && !string.IsNullOrWhiteSpace(name) && stock.Name != name)
            {
                stock.Name = name;
                Save();
            }
        }

        private void Load()
        {
            if (!File.Exists(_filePath)) return;
            try
            {
                var serializer = new XmlSerializer(typeof(StockCodeList));
                using (var reader = new StreamReader(_filePath, System.Text.Encoding.UTF8))
                {
                    var list = (StockCodeList)serializer.Deserialize(reader);
                    _stocks = list?.Items ?? new List<StockCode>();
                }
            }
            catch
            {
                _stocks = new List<StockCode>();
            }
        }

        public void Save()
        {
            var list = new StockCodeList { Items = _stocks };
            var serializer = new XmlSerializer(typeof(StockCodeList));
            using (var writer = new StreamWriter(_filePath, false, System.Text.Encoding.UTF8))
            {
                serializer.Serialize(writer, list);
            }
        }
    }
}
