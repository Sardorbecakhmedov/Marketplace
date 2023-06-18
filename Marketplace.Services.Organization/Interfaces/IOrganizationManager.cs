using Marketplace.Services.Organization.Models;
using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;
using Marketplace.Services.Organization.Models.ViewModels;

namespace Marketplace.Services.Organization.Interfaces;

public interface IOrganizationManager
{
    Task<OrganizationViewModel> CreateAsync(IFormFile? fileModel, CreateOrganizationModel orgModel);
    Task<OrganizationViewModel> UpdateAsync(Guid organizationId, UpdateOrganizationModel model);
    Task<IEnumerable<OrganizationViewModel>> GetAllOrganizations();
    Task<OrganizationViewModel> GetOrganizationAsync(Guid organizationId);
    Task<bool> DeleteAsync(Guid organizationId);
}