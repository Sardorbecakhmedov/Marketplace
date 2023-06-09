using System.ComponentModel.DataAnnotations;

namespace Marketplace.Services.Identity.Models;

public class UpdateUserModel
{
    public string? Username { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
}