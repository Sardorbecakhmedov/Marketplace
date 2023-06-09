using Marketplace.Services.Identity.Entities;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Identity.IdentityContext;

public class IdentityDbContext : DbContext
{
    public DbSet<User> Users => Set<User>();

    public IdentityDbContext(DbContextOptions<IdentityDbContext> options) : base(options) { }
}