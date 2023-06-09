using Marketplace.Common.Providers;
using Marketplace.Services.Chat.ChatContext;
using Marketplace.Services.Chat.Entities;
using Marketplace.Services.Chat.Interfaces;
using Marketplace.Services.Chat.Models;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.Managers;

public class ChatManager : IChatManager
{
    private readonly ChatDbContext _dbContext;
    private readonly UserProvider _userProvider;

    public ChatManager(ChatDbContext dbContext, UserProvider userProvider)
    {
        _dbContext = dbContext;
        _userProvider = userProvider;
    }

    public async Task SaveMessageAsync(NewMessageModel model)
    {
        var chat = GetOrCreateUserChat(model);

        var message = new Message()
        {
            ChatId = chat.Id,
            MessageText = model.MessageText,
            FromUserId = _userProvider.UserId
        };

        await _dbContext.Messages.AddAsync(message);
        await _dbContext.SaveChangesAsync();
    }

    private Entities.Chat GetOrCreateUserChat(NewMessageModel model)
    {
        var chat = _dbContext.Chats.FirstOrDefault(chat => 
            chat.UserIds.Contains(_userProvider.UserId) && chat.UserIds.Contains(model.ToUserId));

        if (chat is not null)
            return chat;

        chat = new Entities.Chat
        {
            ToUsername = model.ToUsername,
            UserIds = new List<Guid>()
            {
                _userProvider.UserId,
                model.ToUserId,
            }
        };

        _dbContext.Chats.Add(chat);
        _dbContext.SaveChanges();

        return chat;
    }

    public async Task<List<MessageModel>> GetChatMessagesAsync(Guid chatId)
    {
        var messages = await _dbContext.Messages
            .Where(msg => msg.ChatId == chatId).ToListAsync();

        return messages.Select(message => new MessageModel(message)).ToList();
    }

    public async Task<List<ChatModel>> GetChatsAsync(Guid userId)
    {
        var userChats = await _dbContext.Chats.Where(chat =>
            chat.UserIds.Contains(userId)).ToListAsync();

        return userChats.Select(chat => new ChatModel
        {
            Id = chat.Id,
            ToUsername = chat.ToUsername,
            FromUserId = chat.UserIds.First(id => id != userId)
        }).ToList();
    }
}