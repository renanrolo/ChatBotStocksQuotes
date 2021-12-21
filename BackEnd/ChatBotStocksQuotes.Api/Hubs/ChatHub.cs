using System.Threading.Tasks;
using ChatBotStocksQuotes.Api.Models;
using Microsoft.AspNetCore.SignalR;

namespace ChatBotStocksQuotes.Api.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message);
        }
    }
}
