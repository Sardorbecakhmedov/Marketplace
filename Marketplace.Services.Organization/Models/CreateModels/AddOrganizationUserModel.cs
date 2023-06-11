using Marketplace.Services.Organization.Entities;

namespace Marketplace.Services.Organization.Models.CreateModels;

public class AddOrganizationUserModel
{
    public required Guid UserId { get; set; }
    public required Guid OrganizationId { get; set; }
    public required OrganizationUserRole UserRole { get; set; }
}   