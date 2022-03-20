using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.Models;
using System;
using System.Collections.Generic;

namespace ChatBotStocksQuotes.Core.Implementations
{
    public class ChatService : IChatService
    {
        private readonly IChatProvider _chatProvider;
        private readonly IChatRepository _chatRepository;

        public ChatService(IChatProvider chatProvider, IChatRepository chatData)
        {
            _chatProvider = chatProvider;
            _chatRepository = chatData;
        }

        public IEnumerable<Chat> FindAll()
        {
            return _chatRepository.FindAll();
        }

        public Chat NewChat(string chatName, string userId = null)
        {
            var chatId = Guid.NewGuid();

            if (!String.IsNullOrEmpty(userId))
            {
                _chatProvider.CreateChat(chatId, userId);
            }

            var chat = new Chat
            {
                Id = chatId,
                Name = chatName
            };

            return _chatRepository.Save(chat);
        }

        public Chat SignIn(Guid chatUuid, string userId)
        {
            var chat = _chatRepository.Find(chatUuid);

            if (chat == null)
            {
                return null;
            }

            _chatProvider.CreateChat(chatUuid, userId);

            return chat;
        }

        public void SendMessage(ChatMessage chatMessage)
        {
            _chatProvider.SendMessageToChatRoom(chatMessage);
        }
    }
}
