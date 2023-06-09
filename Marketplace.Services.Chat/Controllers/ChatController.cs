using Marketplace.Services.Chat.Interfaces;
using Marketplace.Services.Chat.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Chat.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ChatController : ControllerBase
{
    private readonly IChatManager _chatManager;

    public ChatController(IChatManager chatManager)
    {
        _chatManager = chatManager;
    }

    [HttpPost]
    public async Task<IActionResult> SaveMessageAsync([FromForm] NewMessageModel model)
    {
        if (!ModelState.IsValid)
        {
            ModelState.AddModelError("New error", "Invalid input!");
            return BadRequest(ModelState);
        }
        await _chatManager.SaveMessageAsync(model);
        return Ok();
    }

    [HttpGet("get_chats/{userId}")]
    public async Task<List<ChatModel>> GetChatsAsync(Guid userId)
    {
        return await _chatManager.GetChatsAsync(userId);
    }

    [HttpGet("get_messages/{chatId}")]
    public async Task<List<MessageModel>> GetChatMessagesAsync(Guid chatId)
    {
        return await _chatManager.GetChatMessagesAsync(chatId);
    }
}