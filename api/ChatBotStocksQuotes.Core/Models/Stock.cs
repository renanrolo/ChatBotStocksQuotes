using System;
using System.Globalization;
using System.Text;

namespace ChatBotStocksQuotes.Core.Models
{
    public class Stock
    {
        /// <summary>
        /// Exemple: 
        /// Symbol,Date,Time,Open,High,Low,Close,Volume
        /// AAPL.US,2021-12-22,17:36:18,173.04,174.525,172.15,173.885,27996486
        /// </summary>
        /// <param name="line"></param>
        public Stock(string line)
        {
            var splitedLine = line.Split(",");

            Symbol = splitedLine[0];

            var date = new StringBuilder().Append(splitedLine[1]).Append(' ').Append(splitedLine[2]).ToString();
            Date = Convert.ToDateTime(date);
            Open = Convert.ToDecimal(splitedLine[3], new CultureInfo("en-US"));
            High = Convert.ToDecimal(splitedLine[4], new CultureInfo("en-US"));
            Low = Convert.ToDecimal(splitedLine[5], new CultureInfo("en-US"));
            Close = Convert.ToDecimal(splitedLine[6], new CultureInfo("en-US"));
            Volume = Convert.ToInt32(splitedLine[7]);
        }

        public string Symbol { get; set; }
        public DateTime Date { get; set; }
        public decimal Open { get; set; }
        public decimal High { get; set; }
        public decimal Low { get; set; }
        public decimal Close { get; set; }
        public int Volume { get; set; }
    }
}
