using System;

namespace ChatBotStocksQuotes.Core.Models
{
    public class Message : MessageBase
    {
        public DateTime SentDate { get; set; }
        public string From { get; set; }
        public string Text { get; set; }

    }
}
