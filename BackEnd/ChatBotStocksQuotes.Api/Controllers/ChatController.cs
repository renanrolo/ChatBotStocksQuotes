﻿using ChatBotStocksQuotes.Api.Models;
using ChatBotStocksQuotes.Core.Entities;
using ChatBotStocksQuotes.Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace ChatBotStocksQuotes.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ChatController : ControllerBase
    {
        private readonly IChatService _chatService;

        public ChatController(IChatService chat)
        {
            _chatService = chat;
        }

        [HttpGet]
        public IEnumerable<Chat> GetAll()
        {
            return _chatService.FindAll();
        }

        [HttpPost]
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
    }
}
