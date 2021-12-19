using System;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChat
    {
        Guid NewChat(string userId);
    }
}
