using ChatBotStocksQuotes.Core.Entities;
using System;
using System.Collections.Generic;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChatService
    {
        Chat NewChat(string chatName, string userId);
        Chat SignIn(Guid chatUuid, string userId);
        IEnumerable<Chat> FindAll();
    }
}
