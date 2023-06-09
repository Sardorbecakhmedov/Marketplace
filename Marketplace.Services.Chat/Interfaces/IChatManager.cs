using Marketplace.Services.Chat.Models;

namespace Marketplace.Services.Chat.Interfaces;

public interface IChatManager
{
    Task SaveMessageAsync(NewMessageModel model);
    Task<List<MessageModel>> GetChatMessagesAsync(Guid chatId);
    Task<List<ChatModel>> GetChatsAsync(Guid userId);
}   