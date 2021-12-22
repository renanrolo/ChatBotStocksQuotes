using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.MessageBroker.Implementations;
using ChatBotStocksQuotes.Core.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Bot
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMqUow _rabbitMqUow;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly IChatProvider _chatProvider;
        private const string stockCommand = "/stock=";

        public Worker(RabbitMqUow rabbitMqUow, RabbitMqConfig rabbitMqConfig, IChatProvider chatProvider)
        {
            _rabbitMqUow = rabbitMqUow;
            _rabbitMqConfig = rabbitMqConfig;
            _chatProvider = chatProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var queueName = "bot";
                var botTopic = "*.all";

                _rabbitMqUow.Chanel.QueueDeclare(
                     queue: queueName,
                     durable: true,
                     exclusive: false,
                     autoDelete: false,
                     arguments: BuildQueueArguments()
                 );

                _rabbitMqUow.Chanel.QueueBind(queueName, _rabbitMqConfig.Exchange, botTopic, null);

                _rabbitMqUow.KeepListening(queueName, "bot", (ChatMessage message) =>
                {
                    var botResponse = ProcessUserMessage(message);

                    if (botResponse != null)
                    {
                        _chatProvider.SendMessageToUsers(botResponse);
                    }
                });

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private ChatMessage ProcessUserMessage(ChatMessage chatMessage)
        {
            if (!chatMessage.Message.StartsWith(stockCommand))
            {
                //return null;

                Console.WriteLine($"NOT: {chatMessage.Message}");

                return new ChatMessage
                {
                    ChatId = chatMessage.ChatId,
                    From = "bot",
                    Message = $"NOT: {chatMessage.Message}"
                };
            }

            Console.WriteLine($"Command identified on message: {chatMessage.Message}");

            var stockCode = chatMessage.Message.Replace(stockCommand, "");

            return new ChatMessage
            {
                ChatId = chatMessage.ChatId,
                From = "bot",
                Message = $"Hello {chatMessage.From}, I got your question, I'll look for {stockCode} right away"
            };
        }

        private Dictionary<string, object> BuildQueueArguments()
        {
            var queueParams = new Dictionary<string, object>();

            queueParams.Add("x-queue-mode", "lazy");

            queueParams.Add("x-max-length", _rabbitMqConfig.QueueMaxLength);

            return queueParams;
        }
    }
}
