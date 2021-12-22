using System;

namespace ChatBotStocksQuotes.Api.Models
{
    public class UserTokenResponse
    {
        public string Token { get; set; }
        public DateTime Expiration { get; set; }
        public string Id { get; set; }
        public string Email { get; set; }
    }
}
