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
public class OrganizationController : ControllerBase
{
    private readonly IOrganizationManager _organizationManager;

    public OrganizationController(IOrganizationManager organizationManager)
    {
        _organizationManager = organizationManager;
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrganizationAsync([FromForm] CreateOrganizationModel model)
    {
        return Ok(await _organizationManager.CreateAsync(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetAllOrganizationsAsync()
    {
        return Ok(await _organizationManager.GetAllOrganizations());
    }

    [HttpGet("{organizationId}")]
    public async Task<IActionResult> GetOrganizationAsync(Guid organizationId)
    {
        return Ok(await _organizationManager.GetOrganizationAsync(organizationId));
    }

    [HttpPut]
    [AuthorizeOwner]
    public async Task<IActionResult> UpdateOrganizationAsync(Guid organizationId, [FromForm] UpdateOrganizationModel model)
    {
        return Ok(await _organizationManager.UpdateAsync(organizationId, model));
    }

    [HttpDelete("{organizationId}")]
    [AuthorizeOwner]
    public async Task<IActionResult> DeleteOrganizationAsync(Guid organizationId)
    {
        var result = await _organizationManager.DeleteAsync(organizationId);

        if (!result)
            return BadRequest("Failed to delete");

        return Ok("Success, Organization deleted!");
    }
}