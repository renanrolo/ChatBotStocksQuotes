using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.MessageBroker.Config;
using ChatBotStocksQuotes.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;

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
            string queueName = CreateTopic(chatId, user);
            var bindinUsers = CreateTopic(chatId, "users");
            var bindinToAllChat = CreateTopic(chatId, "all");

            _rabbitMqUow.Chanel.QueueDeclare(
                 queue: queueName,
                 durable: true,
                 exclusive: false,
                 autoDelete: false,
                 arguments: BuildQueueArguments()
             );

            _rabbitMqUow.Chanel.QueueBind(queueName, _rabbitMqConfig.Exchange, queueName, null);
            _rabbitMqUow.Chanel.QueueBind(queueName, _rabbitMqConfig.Exchange, bindinUsers, null);
            _rabbitMqUow.Chanel.QueueBind(queueName, _rabbitMqConfig.Exchange, bindinToAllChat, null);
        }

        public bool ChatExists(Guid chatId)
        {
            throw new NotImplementedException();
        }

        public void KeepListening(string queueName, string consumerTag, Action<ChatMessage> callback)
        {
            _rabbitMqUow.KeepListening(queueName, consumerTag, callback);
        }

        public void CancelListening(string consumerTag)
        {
            _rabbitMqUow.CancelListening(consumerTag);
        }

        public void SendMessageToChatRoom(ChatMessage chatMessage)
        {
            var topic = CreateTopic(chatMessage.ChatId, "all");
            _rabbitMqUow.Push(topic, chatMessage);
        }

        public void SendMessageToUsers(ChatMessage chatMessage)
        {
            var topic = CreateTopic(chatMessage.ChatId, "users");
            _rabbitMqUow.Push(topic, chatMessage);
        }

        private string CreateTopic(object chatId, string user)
        {
            return new StringBuilder().Append(chatId)
                                      .Append('.')
                                      .Append(user)
                                      .ToString();
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
