using ChatBotStocksQuotes.Core.Models;
using System;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChatProvider
    {
        void CreateChat(Guid chatId, string userId);
        bool ChatExists(Guid chatId);
        void KeepListening<T>(string queueName, string consumerTag, Action<T> callback) where T : MessageBase;
        void SendMessage(string topic, object data);
        void CancelListening(string consumerTag);
    }
}
