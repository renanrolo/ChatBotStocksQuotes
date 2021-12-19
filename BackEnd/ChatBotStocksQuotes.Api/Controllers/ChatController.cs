using ChatBotStocksQuotes.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Security.Claims;

namespace ChatBotStocksQuotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChat _chat;

        public ChatController(IChat chat)
        {
            _chat = chat;
        }

        [HttpPost("new-chat")]
        public NewChat NewChat()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var chatUuid = _chat.NewChat(userId);

            return new NewChat
            {
                ChatUrl = "olaola",
                ChatUuid = chatUuid
            };
        }
    }


    public class NewChat
    {
        public Guid ChatUuid { get; set; }
        public string ChatUrl { get; set; }
    }
}
