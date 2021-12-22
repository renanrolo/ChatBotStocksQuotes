using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;

namespace ChatBotStocksQuotes.Core.Implementations
{
    public class StockClient : IStockClient
    {
        private readonly IHttpClientFactory _clientFactory;
        private const string url = "https://stooq.com/q/l/?s={0}&f=sd2t2ohlcv&h&e=csv";

        public StockClient(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<Stock> GetStockQuote(string stockCode)
        {
            var stockUrl = String.Format(url, stockCode);

            var request = new HttpRequestMessage(HttpMethod.Get, stockUrl);

            request.Headers.Add("User-Agent", "ChatBotPoC");
            request.Headers.Add("Accept-Encoding", "gzip, deflate, br");

            var client = _clientFactory.CreateClient();

            var response = await client.SendAsync(request);

            if (response.IsSuccessStatusCode)
            {
                var stream = await response.Content.ReadAsStreamAsync();

                using (var reader = new StreamReader(stream))
                {
                    var isFirstLine = true;

                    while (!reader.EndOfStream)
                    {
                        var line = reader.ReadLine();

                        if (isFirstLine)
                        {
                            isFirstLine = false;
                            continue;
                        }

                        if (line.Contains("N/D"))
                        {
                            return null;
                        }

                        return new Stock(line);
                    }
                }
            }

            return null;
        }
    }
}
