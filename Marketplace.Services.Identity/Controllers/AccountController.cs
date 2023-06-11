using Marketplace.Common.Helper;
using Marketplace.Services.Identity.Interfaces;
using Marketplace.Services.Identity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Identity.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly IUserManager _userManager;
    private readonly UserProvider _userProvider;

    public AccountController(IUserManager userManager, UserProvider userProvider)
    {
        _userManager = userManager;
        _userProvider = userProvider;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromForm] RegisterUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var user = await _userManager.RegisterAsync(model);

        var userModel = _userManager.MapToUserModel(user);

        return Ok(userModel);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromForm] LoginUserModel model)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var jwtToken = await _userManager.LoginAsync(model);

        return Ok(jwtToken);
    }

    [HttpGet("get_users")]
    public async Task<IActionResult> GetAllUsers()
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var users = await _userManager.GetAllUsersAsync();

        var userModels = new List<UserCloneModel>();

        foreach (var user in users)
        {
            userModels.Add(_userManager.MapToUserModel(user));
        }

        return Ok(userModels);
    }

    [HttpGet("get_user/{userName}")]
    public async Task<IActionResult> GetUser(string userName)
    {
        var user = await _userManager.GetUserAsync(userName);

        var userModel = _userManager.MapToUserModel(user);

        return Ok(userModel);
    }

    [HttpGet("get_user/{userId}")]
    public async Task<IActionResult> GetUser(Guid userId)
    {
        var user = await _userManager.GetUserAsync(userId);
        var userModel = _userManager.MapToUserModel(user);

        return Ok(userModel);
    }

    [HttpPut("update")]
    [Authorize]
    public async Task<IActionResult> Update([FromForm] UpdateUserModel model)
    {
        var user = await _userManager.UpdateAsync(model);
        var userModel = _userManager.MapToUserModel(user);

        return Ok(userModel);
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> Profile()
    {
        var user = await _userManager.GetUserAsync(_userProvider.UserId);

        var userModel = _userManager.MapToUserModel(user);

        return Ok(userModel);
    }

    [HttpDelete("{userId}")]
    [Authorize]
    public async Task<IActionResult> Delete(Guid userId)
    {
        await _userManager.DeleteAsync(userId);
        return Ok();
    }
}