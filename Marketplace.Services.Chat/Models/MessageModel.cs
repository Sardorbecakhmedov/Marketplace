using System.CodeDom;
using Marketplace.Services.Chat.Entities;

namespace Marketplace.Services.Chat.Models;

public class MessageModel
{
    public Guid Id { get; set; }

    public Guid FromUserId { get; set; }

    public string MessageText { get; set; }
    public DateTime CreatedAt { get; set; }

    public MessageModel(Message message)
    {
        Id = message.Id;
        FromUserId = message.FromUserId;
        MessageText = message.MessageText;
        CreatedAt = message.CreatedAt;
    }
}