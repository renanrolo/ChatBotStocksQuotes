namespace ChatBotStocksQuotes.Api.Models
{
    public class LoginRequest : UserIdentificationBase
    {
        public bool RememberMe { get; set; }
    }
}
