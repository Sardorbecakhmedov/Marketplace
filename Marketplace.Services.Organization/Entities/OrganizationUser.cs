namespace Marketplace.Services.Organization.Entities;

public class OrganizationUser
{
    public Guid Id { get; set; }

    public Guid OrganizationId { get; set; }
    public virtual Organization? Organization { get; set; }

    public Guid UserId { get; set;}
    public OrganizationUserRole OrganizationUserRole { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
}