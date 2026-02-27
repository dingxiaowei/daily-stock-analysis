using System;
using System.Collections.Generic;
using System.IO;
using StockDatasCollection.Models;

namespace StockDatasCollection.IO
{
    /// <summary>
    /// Reads binary archive files (.bin) written by ArchiveService
    /// and reconstructs StockDataPoint records for display/verification.
    /// </summary>
    public class ArchiveDataLoader
    {
        private static readonly char[] ExpectedMagic = { 'S', 'T', 'D', 'A' };

        /// <summary>
        /// Loads all records from a single .bin archive file.
        /// Throws InvalidDataException if magic bytes or version don't match.
        /// </summary>
        public List<StockDataPoint> Load(string filePath)
        {
            using (var fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            using (var br = new BinaryReader(fs, System.Text.Encoding.UTF8))
            {
                // Validate header
                char[] magic = br.ReadChars(4);
                if (magic[0] != 'S' || magic[1] != 'T' || magic[2] != 'D' || magic[3] != 'A')
                    throw new InvalidDataException($"文件格式无效：{filePath}（magic bytes 不匹配）");

                byte version = br.ReadByte();
                if (version != 1)
                    throw new InvalidDataException($"不支持的版本号：{version}");

                string stockCode = br.ReadString();
                int count = br.ReadInt32();

                var points = new List<StockDataPoint>(count);
                for (int i = 0; i < count; i++)
                {
                    var p = new StockDataPoint
                    {
                        StockCode = stockCode,
                        CollectedAt = new DateTime(br.ReadInt64()),
                        StockName = br.ReadString(),
                        OpenPrice = br.ReadString(),
                        PreClosePrice = br.ReadString(),
                        CurrentPrice = br.ReadString(),
                        HighPrice = br.ReadString(),
                        LowPrice = br.ReadString(),
                        BidPrice = br.ReadString(),
                        AskPrice = br.ReadString(),
                        Volume = br.ReadInt64(),
                        Turnover = br.ReadString(),
                        Buy1Vol = br.ReadInt64(), Buy1Price = br.ReadString(),
                        Buy2Vol = br.ReadInt64(), Buy2Price = br.ReadString(),
                        Buy3Vol = br.ReadInt64(), Buy3Price = br.ReadString(),
                        Buy4Vol = br.ReadInt64(), Buy4Price = br.ReadString(),
                        Buy5Vol = br.ReadInt64(), Buy5Price = br.ReadString(),
                        Sell1Vol = br.ReadInt64(), Sell1Price = br.ReadString(),
                        Sell2Vol = br.ReadInt64(), Sell2Price = br.ReadString(),
                        Sell3Vol = br.ReadInt64(), Sell3Price = br.ReadString(),
                        Sell4Vol = br.ReadInt64(), Sell4Price = br.ReadString(),
                        Sell5Vol = br.ReadInt64(), Sell5Price = br.ReadString(),
                        TradeDate = br.ReadString(),
                        TradeTime = br.ReadString(),
                        StatusCode = br.ReadString()
                    };
                    points.Add(p);
                }
                return points;
            }
        }

        /// <summary>
        /// Loads all .bin files from a directory (recursively) and aggregates results.
        /// Returns Tuple where Item1 = all records, Item2 = list of errors per file.
        /// </summary>
        public Tuple<List<StockDataPoint>, List<string>> LoadDirectory(string directory)
        {
            var allPoints = new List<StockDataPoint>();
            var errors = new List<string>();

            foreach (string file in Directory.GetFiles(directory, "*.bin", SearchOption.AllDirectories))
            {
                try
                {
                    var points = Load(file);
                    allPoints.AddRange(points);
                }
                catch (Exception ex)
                {
                    errors.Add(Path.GetFileName(file) + ": " + ex.Message);
                }
            }
            return Tuple.Create(allPoints, errors);
        }
    }
}
