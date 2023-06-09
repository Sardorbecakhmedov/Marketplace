namespace Marketplace.Services.Chat.Entities;

public class Chat
{
    public Guid Id { get; set; }

    public string ToUsername { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public List<Guid> UserIds { get; set; } = new ();
    public List<Message> Messages { get; set; } = new ();
}