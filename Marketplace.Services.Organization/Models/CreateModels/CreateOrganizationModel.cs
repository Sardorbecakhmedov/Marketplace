namespace Marketplace.Services.Organization.Models.CreateModels;

public class CreateOrganizationModel
{
    public string OrganizationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public virtual List<CreateOrganizationAddressModel>? Addresses { get; set; }
}