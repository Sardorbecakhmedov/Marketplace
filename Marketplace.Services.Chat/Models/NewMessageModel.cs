namespace Marketplace.Services.Chat.Models;

public class NewMessageModel
{
    public Guid ToUserId { get; set; }
    public string ToUsername { get; set; } = null!;
    public string MessageText { get; set; } = null!;
}