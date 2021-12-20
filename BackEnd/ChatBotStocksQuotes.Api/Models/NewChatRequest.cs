using System.Collections.Generic;
using System;

namespace ChatBotStocksQuotes.Api.Models
{
    public class NewChatRequest
    {
        public string Name { get; set; }

        public bool Validate(out List<string> errors)
        {
            errors = new List<string>();
            if (String.IsNullOrWhiteSpace(Name) || Name.Length < 4 || Name.Length >= 20)
            {
                errors.Add("Name should have more than 4 chars and less than 20.");
                return false;
            }

            return true;
        }
    }
}
