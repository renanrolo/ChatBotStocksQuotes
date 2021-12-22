using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.MessageBroker.Implementations;
using ChatBotStocksQuotes.Core.Models;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Bot
{
    public class Worker : BackgroundService
    {
        private readonly RabbitMqUow _rabbitMqUow;
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly IChatProvider _chatProvider;
        private readonly IStockClient _stockClient;
        private const string _stockCommand = "/stock=";
        private const string _quoteTemplate = "{0} quote is ${1} per share";

        public Worker(RabbitMqUow rabbitMqUow, RabbitMqConfig rabbitMqConfig, IChatProvider chatProvider, IHttpClientFactory clientFactory, IStockClient stockClient)
        {
            _rabbitMqUow = rabbitMqUow;
            _rabbitMqConfig = rabbitMqConfig;
            _chatProvider = chatProvider;
            _stockClient = stockClient;
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

                _rabbitMqUow.KeepListening(queueName, "bot", async (ChatMessage message) =>
                {
                    var botResponse = await ProcessUserMessage(message);

                    if (botResponse != null)
                    {
                        _chatProvider.SendMessageToUsers(botResponse);
                    }
                });

                await Task.Delay(TimeSpan.FromDays(1), stoppingToken);
            }
        }

        private async Task<ChatMessage> ProcessUserMessage(ChatMessage chatMessage)
        {
            try
            {
                if (!chatMessage.Message.StartsWith(_stockCommand))
                {
                    Console.WriteLine($"NOT: {chatMessage.Message}");
                    return null;
                }

                Console.WriteLine($"Command identified on message: {chatMessage.Message}");

                var stockCode = chatMessage.Message.Replace(_stockCommand, "");

                var stock = await _stockClient.GetStockQuote(stockCode);

                if (stock != null)
                {
                    return InformStockQuote(chatMessage, stock);
                }

                return new ChatMessage
                {
                    ChatId = chatMessage.ChatId,
                    From = "bot",
                    Message = $"Sorry {chatMessage.From}, I couldn't find any quote for {stockCode}"
                };
            }
            catch
            {
                return new ChatMessage
                {
                    ChatId = chatMessage.ChatId,
                    From = "bot",
                    Message = $"{chatMessage.From}, I was unable to search for quotes, please try again."
                };
            }
        }

        private static ChatMessage InformStockQuote(ChatMessage chatMessage, Stock stock)
        {
            var response = String.Format(_quoteTemplate, stock.Symbol, stock.Close);

            return new ChatMessage
            {
                ChatId = chatMessage.ChatId,
                From = "bot",
                Message = response
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
