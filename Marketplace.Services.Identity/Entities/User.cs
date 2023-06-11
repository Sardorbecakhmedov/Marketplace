using Microsoft.VisualBasic;

namespace Marketplace.Services.Identity.Entities;

public class User
{
    public Guid Id { get; set; }
    public required string Username { get; set; } 
    public required string Email { get; set; }
    public bool IsDeleted { get; set; }
    public UserRole UserRole { get; set; }
    public string PasswordHash { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}