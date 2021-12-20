using System.ComponentModel.DataAnnotations;

namespace ChatBotStocksQuotes.Api.Models
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}
