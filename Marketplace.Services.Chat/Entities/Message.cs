namespace Marketplace.Services.Chat.Entities;

public class Message
{
    public Guid Id { get; set; }

    public Guid ChatId { get; set; }
    public Chat? Chat { get; set; }

    public Guid FromUserId { get; set; }

    public required string MessageText { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}