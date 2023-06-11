namespace Marketplace.Services.Organization.Entities;

public class OrganizationAddress
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }
    public string Address { get; set; } = null!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
}