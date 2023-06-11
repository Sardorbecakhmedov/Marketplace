namespace Marketplace.Services.Organization.Entities;

public class Organization
{
    public Guid Id { get; set; }
    public string OrganizationName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public string? LogoPath { get; set; }
    public string Email { get; set; } = null!;
    public string PhoneNumber { get; set; } = null!;
    public bool IsDeleted { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    public virtual List<OrganizationUser>? OrganizationUsers { get; set; }
    public virtual List<OrganizationAddress>? OrganizationAddresses { get; set; }

}