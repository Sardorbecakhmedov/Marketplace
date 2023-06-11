namespace Marketplace.Services.Organization.Models.UpdateModels;

public class UpdateOrganizationModel
{
    public string? OrganizationName { get; set; } = null!;
    public string? Description { get; set; } = null!;
    public string? LogoFilePath { get; set; }
    public string? Email { get; set; } = null!;
    public string? PhoneNumber { get; set; } = null!;
    public virtual List<UpdateOrganizationAddressModel>? Addresses { get; set; }
}