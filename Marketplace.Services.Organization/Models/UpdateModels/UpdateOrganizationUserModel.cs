using Marketplace.Services.Organization.Entities;

namespace Marketplace.Services.Organization.Models.UpdateModels;

public class UpdateOrganizationUserModel
{
    public Guid? UserId { get; set; }
    public Guid? OrganizationId { get; set; }
    public OrganizationUserRole? UserRole { get; set; }
}   