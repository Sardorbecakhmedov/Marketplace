namespace Marketplace.Services.Chat.Models;

public class ChatModel
{
    public Guid Id { get; set; }
    public string ToUsername { get; set; } = null!;
    public Guid FromUserId { get; set; }
}