using System;

namespace StockDatasCollection.Models
{
    /// <summary>
    /// Represents one snapshot of stock data collected from Sina Finance API.
    /// Field indices correspond to the comma-separated payload from hq.sinajs.cn.
    /// </summary>
    public class StockDataPoint
    {
        // --- Metadata (added by this system) ---
        public string StockCode { get; set; }      // e.g. "sh600519"
        public DateTime CollectedAt { get; set; }  // local system time of collection

        // --- API fields (stored as strings to preserve exact decimal representation) ---
        public string StockName { get; set; }      // [0]  e.g. "贵州茅台"
        public string OpenPrice { get; set; }      // [1]  今日开盘价
        public string PreClosePrice { get; set; }  // [2]  昨日收盘价
        public string CurrentPrice { get; set; }   // [3]  当前价（最新成交价）
        public string HighPrice { get; set; }      // [4]  今日最高价
        public string LowPrice { get; set; }       // [5]  今日最低价
        public string BidPrice { get; set; }       // [6]  竞买价
        public string AskPrice { get; set; }       // [7]  竞卖价
        public long   Volume { get; set; }         // [8]  成交量（股）
        public string Turnover { get; set; }       // [9]  成交额（元）

        // --- Buy 1-5 (volume + price) [10..19] ---
        public long   Buy1Vol { get; set; }
        public string Buy1Price { get; set; }
        public long   Buy2Vol { get; set; }
        public string Buy2Price { get; set; }
        public long   Buy3Vol { get; set; }
        public string Buy3Price { get; set; }
        public long   Buy4Vol { get; set; }
        public string Buy4Price { get; set; }
        public long   Buy5Vol { get; set; }
        public string Buy5Price { get; set; }

        // --- Sell 1-5 (volume + price) [20..29] ---
        public long   Sell1Vol { get; set; }
        public string Sell1Price { get; set; }
        public long   Sell2Vol { get; set; }
        public string Sell2Price { get; set; }
        public long   Sell3Vol { get; set; }
        public string Sell3Price { get; set; }
        public long   Sell4Vol { get; set; }
        public string Sell4Price { get; set; }
        public long   Sell5Vol { get; set; }
        public string Sell5Price { get; set; }

        // --- Trailing fields ---
        public string TradeDate { get; set; }   // [30] e.g. "2026-02-26"
        public string TradeTime { get; set; }   // [31] e.g. "15:00:04"
        public string StatusCode { get; set; }  // [32] e.g. "00"
    }
}
