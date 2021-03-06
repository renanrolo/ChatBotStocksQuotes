using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Infra.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatBotStocksQuotes.Infra.Data.Repositories
{
    public class ChatRepository : IChatRepository
    {
        private readonly ApplicationDbContext _context;

        public ChatRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Chat Find(Guid chatUuid)
        {
            return _context.Chats.FirstOrDefault(x => x.Id == chatUuid);
        }

        public IEnumerable<Chat> FindAll()
        {
            return _context.Chats.ToList();
        }

        public Chat Save(Chat chat)
        {
            _context.Chats.Add(chat);
            _context.SaveChanges();
            return chat;
        }
    }
}
