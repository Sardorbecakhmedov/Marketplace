using System.Text.Json.Serialization;
using Marketplace.Common.Extensions;
using Marketplace.Common.Helper;
using Marketplace.Services.Chat.ChatContext;
using Marketplace.Services.Chat.Interfaces;
using Marketplace.Services.Chat.Managers;
using Microsoft.EntityFrameworkCore;

namespace Marketplace.Services.Chat.Extensions;

public static class ExtensionsChat
{
    public static void AddChatServices(this IServiceCollection services, IConfiguration configuration)
    {
        AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", isEnabled: true);
        services.AddDbContext<ChatDbContext>(config =>
        {
            config.UseNpgsql(configuration.GetConnectionString("ChatDb"));
        });

        services.AddControllers().AddJsonOptions(config =>
        {
            config.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

        services.AddEndpointsApiExplorer();
        services.AddHttpContextAccessor();
        services.AddSignalR();
        services.AddJwtConfiguration(configuration);
        services.AddSwaggerGenWithToken();
        services.AddScoped<UserProvider>();
        services.AddScoped<IChatManager, ChatManager>();
    }

    public static void AutoMigrateChatDb(this WebApplication app)
    {
        if (app.Services.GetService<ChatDbContext>() != null)
        {
            var chatDb = app.Services.GetRequiredService<ChatDbContext>();
            chatDb.Database.Migrate();
        }
    }
}