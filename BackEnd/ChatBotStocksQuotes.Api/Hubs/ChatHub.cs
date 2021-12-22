using ChatBotStocksQuotes.Api.Models;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.Models;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Hubs
{
    public class ChatHub : Hub<IChatClient>
    {
        private readonly IChatProvider _chatProvider;

        public ChatHub(IChatProvider chatProvider)
        {
            _chatProvider = chatProvider;
        }

        public async Task SendMessage(ChatMessage message)
        {
            await Clients.All.ReceiveMessage(message);
        }

        public Task EnterChat(ChatSignIn chatSignIn)
        {
            var queueName = new StringBuilder()
                                .Append(chatSignIn.ChatId)
                                .Append('.')
                                .Append(chatSignIn.UserId)
                                .ToString();

            var consumerTag = Context.ConnectionId;

            Console.WriteLine($"{consumerTag} - User Entered The Chat");

            _chatProvider.KeepListening(queueName, consumerTag, async (ChatMessage message) =>
            {
                Console.WriteLine($"{consumerTag} - ReceiveMessage: {message.Message}");

                try
                {
                    await Clients.Client(consumerTag).ReceiveMessage(message);
                }
                catch
                {
                    Console.WriteLine($"{consumerTag} - ReceiveMessage ERROR: {message.Message}");
                }

            });

            return Task.CompletedTask;
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            var consumerTag = Context.ConnectionId;

            Console.WriteLine($"{consumerTag} - User disconected");

            _chatProvider.CancelListening(consumerTag);
            return Task.CompletedTask;
        }
    }
}
