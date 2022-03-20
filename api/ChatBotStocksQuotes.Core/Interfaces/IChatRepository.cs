using ChatBotStocksQuotes.Core.Entities;
using System;
using System.Collections.Generic;

namespace ChatBotStocksQuotes.Core.Interfaces
{
    public interface IChatRepository
    {
        Chat Find(Guid chatUuid);
        IEnumerable<Chat> FindAll();
        Chat Save(Chat chat);
    }
}
