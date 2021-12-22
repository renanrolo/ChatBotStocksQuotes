using ChatBotStocksQuotes.Api.Hubs;
using ChatBotStocksQuotes.Api.Models;
using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Interfaces;
using ChatBotStocksQuotes.Core.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChatBotStocksQuotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;
        private readonly IHubContext<ChatHub, IChatClient> _chatHub;

        public ChatController(IChatService chatService, IHubContext<ChatHub, IChatClient> chatHub)
        {
            _chatService = chatService;
            _chatHub = chatHub;
        }

        [HttpGet]
        [Authorize]
        public IEnumerable<Chat> GetAll()
        {
            return _chatService.FindAll();
        }

        [HttpPost]
        [Authorize]
        public IActionResult NewChat(NewChatRequest newChatRequest)
        {
            if (!newChatRequest.Validate(out var errors))
            {
                return BadRequest(new { errors });
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var newChat = _chatService.NewChat(newChatRequest.Name, userId);

            return Ok(new NewChatResponse(newChat));
        }

        [HttpGet("sign-in/{chatid}")]
        [Authorize]
        public IActionResult SignIn(Guid chatid)
        {
            var userId = User.FindFirstValue("id");

            var chat = _chatService.SignIn(chatid, userId);

            if (chat == null)
            {
                return NotFound();
            }

            return Ok(new { 
                chat = chat
            });
        }

        [HttpPost("message")]
        [Authorize]
        public async Task<IActionResult> ReceiveMessage(ChatMessage chatMessage)
        {
            if (!chatMessage.Validate(out var errors))
            {
                return BadRequest(new { errors });
            }

            _chatService.SendMessage(chatMessage);

            return Ok();
        }
    }
}
