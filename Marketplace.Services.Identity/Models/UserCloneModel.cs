using Marketplace.Services.Identity.Entities;

namespace Marketplace.Services.Identity.Models;

public class UserCloneModel
{
    public Guid Id { get; set; }
    public required string Username { get; set; }
    public required string Email { get; set; }
    public UserRole UserRole { get; set; }
    public DateTime CreatedAt { get; set; } 
}