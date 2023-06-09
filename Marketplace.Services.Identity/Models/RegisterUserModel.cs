using System.ComponentModel.DataAnnotations;

namespace Marketplace.Services.Identity.Models;

public class RegisterUserModel
{
    public required string Username { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }

    [Compare(nameof(Password))]
    public required string ConfirmPassword { get; set; }
}