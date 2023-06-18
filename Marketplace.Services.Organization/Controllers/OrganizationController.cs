using Marketplace.Services.Organization.ActionFilters;
using Marketplace.Services.Organization.Interfaces;
using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;
using Marketplace.Services.Organization.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Filters;

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
    
    /*[HttpPost]
    public async Task<ActionResult<OrganizationViewModel>> CreateOrganization([FromForm] IFormFile fileModel,
        [FromBody] CreateOrganizationModel orgModel)
    {
        return Ok(await _organizationManager.CreateAsync(fileModel, orgModel));
    }*/


    [HttpPost("{fileModel}")]
    public async Task<IActionResult> CreateOrganizationAsync(IFormFile? fileModel, [FromBody] CreateOrganizationModel model)
    {
        return Ok(await _organizationManager.CreateAsync(fileModel, model));
    }

  
    [HttpGet]
    [AllowAnonymous]
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


