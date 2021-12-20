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

        public Worker(RabbitMqUow rabbitMqUow, RabbitMqConfig rabbitMqConfig)
        {
            _rabbitMqUow = rabbitMqUow;
            _rabbitMqConfig = rabbitMqConfig;
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

                _rabbitMqUow.KeepListening(queueName, (Message message) => {
                    string topicToUsersOnly = BuildTopicForUser(message);
                    _rabbitMqUow.Push(topicToUsersOnly, $"I read your message {message.From}");
                });

                await Task.Delay(TimeSpan.FromSeconds(10));
            }
        }

        private string BuildTopicForUser(Message message)
        {
            var firstTopic = message.RoutingKey.Split(".")[0];

            return firstTopic + ".users";
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
