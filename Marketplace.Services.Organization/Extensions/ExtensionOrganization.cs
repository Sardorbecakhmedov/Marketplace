using Marketplace.Common.Extensions;
using Marketplace.Services.Organization.OrganizationContext;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using Marketplace.Services.Organization.Interfaces;
using Marketplace.Services.Organization.Managers;
using Marketplace.Common.Helper;
using Marketplace.Common.Interfaces;

namespace Marketplace.Services.Organization.Extensions;

public static class ExtensionOrganization
{
    public static void AddOrganizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);
        services.AddDbContext<OrganizationDbContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("OrganizationDb"));
        });

        services.AddControllers().AddJsonOptions(config =>
        {
            config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddEndpointsApiExplorer();

        services.AddJwtConfiguration(configuration);
        services.AddSwaggerGenWithToken();
        services.AddHttpContextAccessor();
        services.AddScoped<UserProvider>();
        services.AddSingleton<IFileManager,FileManager>();
        services.AddScoped<IOrganizationManager, OrganizationManager>();
    }

    public static void AutoMigrateOrganizationDb(this WebApplication app)
    {
        if (app.Services.GetService<OrganizationDbContext>() != null)
        {
            var organizationDb = app.Services.GetRequiredService<OrganizationDbContext>();
            organizationDb.Database.Migrate();
        }
    }
}