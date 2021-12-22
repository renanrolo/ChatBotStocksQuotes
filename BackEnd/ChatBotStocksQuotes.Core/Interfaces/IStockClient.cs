using ChatBotStocksQuotes.Core.Models;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IStockClient
    {
        Task<Stock> GetStockQuote(string stockCode);
    }
}
