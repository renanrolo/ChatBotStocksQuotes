using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Core.MessageBroker.Implementations
{
    public class ChatProvider : IChatProvider
    {
        private readonly RabbitMqConfig _rabbitMqConfig;
        private readonly RabbitMqUow _rabbitMqUow;

        public ChatProvider(RabbitMqConfig rabbitMqConfig, RabbitMqUow rabbitMqUow)
        {
            _rabbitMqConfig = rabbitMqConfig;
            _rabbitMqUow = rabbitMqUow;
        }


        public void CreateChat(Guid chatId, string userId)
        {
            BindUserToChat(chatId, userId);
        }

        public void BindUserToChat(Guid chatId, string user)
        {
            var topic = new StringBuilder().Append(chatId)
                                           .Append(".user")
                                           .ToString();

            var queueName = new StringBuilder().Append(chatId)
                                           .Append(".")
                                           .Append(user)
                                           .ToString();

            _rabbitMqUow.Chanel.QueueDeclare(
                 queue: queueName,
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: BuildQueueArguments()
             );

            _rabbitMqUow.Chanel.QueueBind(queueName, _rabbitMqConfig.Exchange, topic, null);
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
