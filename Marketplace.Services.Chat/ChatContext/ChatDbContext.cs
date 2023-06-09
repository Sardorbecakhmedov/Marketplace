using Marketplace.Services.Chat.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.ChatContext;

public class ChatDbContext : DbContext
{
    public DbSet<Entities.Chat> Chats => Set<Entities.Chat>();
    public DbSet<Message> Messages => Set<Message>();

    public ChatDbContext(DbContextOptions<ChatDbContext> options) : base(options) { }

}