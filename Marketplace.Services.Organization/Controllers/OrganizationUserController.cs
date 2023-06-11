using System.Runtime.InteropServices;
using Marketplace.Services.Organization.ActionFilters;
using Marketplace.Services.Organization.Interfaces;
using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Organization.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
[AuthorizeOwner]
public class OrganizationUserController : ControllerBase
{
    private readonly IOrganizationUserManager _orgUserManager;

    public OrganizationUserController(IOrganizationUserManager orgUserManager)
    {
        _orgUserManager = orgUserManager;
    }

    [HttpPost]
    public async Task<IActionResult> AddUserAsync([FromForm] AddOrganizationUserModel model)
    {
        return Ok(await _orgUserManager.AddUserAsync(model));
    }

    [HttpGet("{organizationId}")]
    public async Task<IActionResult> GetOrganizationAllUsersAsync(Guid organizationId)
    {
        return Ok(await _orgUserManager.GetOrganizationUsersAsync(organizationId));
    }

    [HttpGet("{userId} {organizationId}")]
    public async Task<IActionResult> GetOrganizationUserAsync(Guid userId, Guid organizationId)
    {
        return Ok(await _orgUserManager.GetOrganizationUserAsync(userId, organizationId));
    }

    [HttpPut("{organizationId} {userId}")]
    public async Task<IActionResult> UpdateOrganizationUserAsync(Guid organizationId, Guid userId,
        [FromForm] UpdateOrganizationUserModel model)
    {
        return Ok(await _orgUserManager.UpdateOrganizationUserAsync(organizationId, userId, model));
    }

    [HttpDelete("{userId} {organizationId}")]
    public async Task<IActionResult> DeleteOrganizationUserAsync(Guid userId, Guid organizationId)
    {
        await _orgUserManager.DeleteOrganizationUserAsync(userId, organizationId);
        return Ok();
    }
}