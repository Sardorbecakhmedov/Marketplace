using Marketplace.Common.Providers;
using Marketplace.Services.Identity.Entities;
using Marketplace.Services.Identity.IdentityContext;
using Marketplace.Services.Identity.Interfaces;
using Marketplace.Services.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Identity.Managers;

public class UserManager : IUserManager
{
    private readonly IdentityDbContext _dbContext;
    private readonly IJwtManager _tokenManager;
    private readonly UserProvider _userProvider;

    public UserManager(IdentityDbContext dbContext, IJwtManager tokenManager, UserProvider userProvider)
    {
        _dbContext = dbContext;
        _tokenManager = tokenManager;
        _userProvider = userProvider;
    }

    public async Task<User> RegisterAsync(RegisterUserModel model)
    {
        if (await _dbContext.Users.AnyAsync(user => user.Username == model.Username))
        {
            throw new Exception("Username already exists!");
        }

        var user = new User
        {
            Username = model.Username,
            Email = model.Email,
            UserRole = UserRole.User,
            IsDeleted = false
        };

        user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        await _dbContext.AddAsync(user);
        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task<string> LoginAsync(LoginUserModel model)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == model.UserName && !user.IsDeleted);

        if (user != null)
        {
            var result = new PasswordHasher<User>()
                .VerifyHashedPassword(user, user.PasswordHash, model.Password);

            if (result != PasswordVerificationResult.Success)
            {
                throw new Exception("User password incorrect!");
            }

            var jwtToken = _tokenManager.CreateJwtToken(user);

            return jwtToken;
        }

        throw new Exception("Username or Password incorrect!");
    }

    public async Task<List<User>> GetAllUsersAsync()
    {
        return await _dbContext.Users.Where(user => !user.IsDeleted).ToListAsync();
    }
        
    public async Task<User> GetUserAsync(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId && !user.IsDeleted);
        return user ?? throw new Exception("User not found!");
    }

    public async Task<User> GetUserAsync(string userName)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Username == userName && !user.IsDeleted);
        return user ?? throw new Exception("User not found!");
    }

    public async Task<User> UpdateAsync(UpdateUserModel model)
    {
        var user = await GetUserAsync(_userProvider.UserId );

        user.Username = model.Username ?? user.Username;
        user.Email = model.Email ?? user.Email;

        if (!string.IsNullOrEmpty(model.Password))
        {
            user.PasswordHash = new PasswordHasher<User>().HashPassword(user, model.Password);
        };

        await _dbContext.SaveChangesAsync();

        return user;
    }

    public async Task DeleteAsync(Guid userId)
    {
        var user = await _dbContext.Users.FirstOrDefaultAsync(user => user.Id == userId && !user.IsDeleted);

        if (user is null)
        {
            throw new Exception("User not found!");
        }
        else
        {
            user.IsDeleted = true;
            await _dbContext.SaveChangesAsync();
        }
    }

    public UserCloneModel MapToUserModel(User user)
    {
        var userModel = new UserCloneModel
        {
            Id = user.Id,
            Email = user.Email,
            Username = user.Username,
            UserRole = user.UserRole,
            CreatedAt = user.CreatedAt,
        };

        return userModel;
    }
}