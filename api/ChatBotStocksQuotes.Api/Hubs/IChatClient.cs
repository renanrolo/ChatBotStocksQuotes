using ChatBotStocksQuotes.Core.Models;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Hubs
{
    public interface IChatClient
    {
        Task ReceiveMessage(ChatMessage message);
    }
}
