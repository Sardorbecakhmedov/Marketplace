using Marketplace.Services.Identity.Entities;
using Marketplace.Services.Identity.Models;

namespace Marketplace.Services.Identity.Interfaces;

public interface IUserManager
{
    Task<User> RegisterAsync(RegisterUserModel model);
    Task<string> LoginAsync(LoginUserModel model);
    Task<List<User>> GetAllUsersAsync();
    Task<User> GetUserAsync(Guid userId);
    Task<User> GetUserAsync(string userName);
    Task<User> UpdateAsync(UpdateUserModel model);
    Task DeleteAsync(Guid userId);
    UserCloneModel MapToUserModel(User user);
}