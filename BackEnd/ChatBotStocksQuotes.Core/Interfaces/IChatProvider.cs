using ChatBotStocksQuotes.Core.Models;
using System;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChatProvider
    {
        void CreateChat(Guid chatId, string userId);
        bool ChatExists(Guid chatId);
        void KeepListening(string queueName, string consumerTag, Action<ChatMessage> callback);
        void SendMessageToChatRoom(ChatMessage chatMessage);

        void SendMessageToUsers(ChatMessage chatMessage);
        void CancelListening(string consumerTag);
    }
}
