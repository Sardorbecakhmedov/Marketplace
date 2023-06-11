using Marketplace.Services.Organization.Entities;

namespace Marketplace.Services.Organization.Models.ViewModels;

public class OrganizationViewUserModel
{
    public Guid UserId { get; set; }
    public OrganizationUserRole UserRole { get; set; }
}   