﻿using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Interfaces;
using System;
using System.Collections.Generic;

namespace ChatBotStocksQuotes.Core.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatProvider _chatProvider;
        private readonly IChatRepository _chatData;

        public ChatService(IChatProvider chatProvider, IChatRepository chatData)
        {
            _chatProvider = chatProvider;
            _chatData = chatData;
        }

        public IEnumerable<Chat> FindAll()
        {
            return _chatData.FindAll();
        }

        public Chat NewChat(string chatName, string userId)
        {
            var chatId = Guid.NewGuid();

            _chatProvider.CreateChat(chatId, userId);

            var chat = new Chat
            {
                Id = chatId,
                Name = chatName
            };

            return _chatData.Save(chat);
        }

        public Guid? SignIn(Guid chatUuid, string userId)
        {
            var chat = _chatData.Find(chatUuid);

            if (chat == null)
            {
                return null;
            }

            _chatProvider.CreateChat(chatUuid, userId);

            return chatUuid;
        }
    }
}
