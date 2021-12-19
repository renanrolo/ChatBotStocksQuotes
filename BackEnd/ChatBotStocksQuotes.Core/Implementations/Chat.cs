using ChatBotStocksQuotes.Core.Interfaces;
using System;

namespace ChatBotStocksQuotes.Core.Implementations
{
    public class Chat : IChat
    {
        private readonly IChatProvider _chatProvider;

        public Chat(IChatProvider chatProvider)
        {
            _chatProvider = chatProvider;
        }

        public Guid NewChat(string userId)
        {
            var chatId = Guid.NewGuid();

            _chatProvider.CreateChat(chatId, userId);

            return chatId;
        }
    }
}
