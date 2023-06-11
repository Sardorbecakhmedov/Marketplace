using Marketplace.Services.Organization.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Organization.OrganizationContext;

public class OrganizationDbContext : DbContext
{
    public DbSet<Entities.Organization> Organizations => Set<Entities.Organization>();
    public DbSet<OrganizationUser> OrganizationUsers => Set<OrganizationUser>();
    public DbSet<OrganizationAddress> OrganizationAddress => Set<OrganizationAddress>();

    public OrganizationDbContext(DbContextOptions<OrganizationDbContext> options) : base(options)
    { }
}