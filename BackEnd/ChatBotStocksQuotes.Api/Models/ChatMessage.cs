//using System;
//using System.Collections.Generic;
//using System.Linq;

//namespace ChatBotStocksQuotes.Api.Models
//{
//    public class ChatMessage
//    {
//        public string ChatId { get; set; }
//        public string From { get; set; }
//        public string Message { get; set; }
//        public DateTime? SentAt { get; set; }

//        public ChatMessage()
//        {
//            SentAt = DateTime.Now;
//        }

//        public bool Validate(out List<string> errors)
//        {
//            errors = new List<string>();

//            if (String.IsNullOrEmpty(ChatId))
//            {
//                errors.Add("Chat identification is required");
//            }

//            if (String.IsNullOrEmpty(From))
//            {
//                errors.Add("Message From is required");
//            }

//            if (String.IsNullOrEmpty(Message))
//            {
//                errors.Add("Message is required");
//            }

//            return !errors.Any();
//        }
//    }
//}
