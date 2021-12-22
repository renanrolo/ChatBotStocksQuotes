using System;
using System.Globalization;
using System.Text;

namespace ChatBotStocksQuotes.Core.Models
{
    public class Stock
    {
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
