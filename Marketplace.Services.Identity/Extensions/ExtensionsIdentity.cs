using Marketplace.Common.Extensions;
using Marketplace.Common.Helper;
using Marketplace.Services.Identity.IdentityContext;
using Marketplace.Services.Identity.Interfaces;
using Marketplace.Services.Identity.Managers;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace Marketplace.Services.Identity.Extensions;

public static class ExtensionsIdentity
{
    public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        services.AddDbContext<IdentityDbContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("IdentityDb"));
        });

        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
        services.AddEndpointsApiExplorer();

        services.AddHttpContextAccessor();
        services.AddSwaggerGenWithToken();
        services.AddJwtConfiguration(configuration);
        services.AddScoped<UserProvider>();
        services.AddScoped<IJwtManager, JwtTokenManager>();
        services.AddScoped<IUserManager, UserManager>();
    }

    public static void AutoMigrateIdentityDb(this WebApplication app)
    {
        if (app.Services.GetService<IdentityDbContext>() != null)
        {
            var identityDb = app.Services.GetRequiredService<IdentityDbContext>();
            identityDb.Database.Migrate();
        }
    }
}