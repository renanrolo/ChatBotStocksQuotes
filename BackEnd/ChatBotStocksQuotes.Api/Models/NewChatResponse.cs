using ChatBotStocksQuotes.Core.Entities;
using System;

namespace ChatBotStocksQuotes.Api.Models
{
    public class NewChatResponse
    {
        public NewChatResponse(Chat chat)
        {
            Id = chat.Id;
            Name = chat.Name;
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
    }
}
