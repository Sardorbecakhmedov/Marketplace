namespace Marketplace.Services.Organization.Models.ViewModels;

public class OrganizationViewModel
{
    public Guid OrganizationId { get; set; }
    public string OrganizationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? LogoPath { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;

    public virtual List<OrganizationViewUserModel>? Users { get; set; }
    public virtual List<OrganizationViewAddressModel>? Addresses { get; set; }
}