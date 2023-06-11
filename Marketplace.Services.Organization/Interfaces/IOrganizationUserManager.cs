using Marketplace.Services.Organization.Models.CreateModels;
using Marketplace.Services.Organization.Models.UpdateModels;

namespace Marketplace.Services.Organization.Interfaces;

public interface IOrganizationUserManager
{
    Task<AddOrganizationUserModel> AddUserAsync (AddOrganizationUserModel orgUserModel);
    Task<IEnumerable<AddOrganizationUserModel>> GetOrganizationUsersAsync(Guid organizationId);
    Task<AddOrganizationUserModel> GetOrganizationUserAsync (Guid userId, Guid organizationId);
    Task<AddOrganizationUserModel> UpdateOrganizationUserAsync(Guid organizationId, Guid userId,
        UpdateOrganizationUserModel model);
    Task DeleteOrganizationUserAsync (Guid userId, Guid organizationId);
}