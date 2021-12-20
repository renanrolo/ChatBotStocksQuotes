using System;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChatProvider
    {
        void CreateChat(Guid chatId, string userId);
        bool ChatExists(Guid chatId);
    }
}
