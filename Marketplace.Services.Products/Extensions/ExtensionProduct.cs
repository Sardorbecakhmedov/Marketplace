using Marketplace.Common.Extensions;
using Marketplace.Common.Helper;
using Marketplace.Common.Interfaces;
using Marketplace.Services.Products.HelperServices;
using Marketplace.Services.Products.Interfaces;
using Marketplace.Services.Products.Managers;

namespace Marketplace.Services.Products.Extensions;

public static class ExtensionProduct
{
    public static void AddProductServices(this IServiceCollection services, 
        IConfiguration configuration)
    {
        services.AddControllers();
        services.AddEndpointsApiExplorer();
        services.AddJwtConfiguration(configuration);
        services.AddSwaggerGenWithToken();

        services.AddScoped<ICategoryManager, CategoryManager>();
        services.AddScoped<IProductManager, ProductManager>();
        services.AddScoped<IFileManager, FileManager>();
        services.AddSingleton<CategoryHelper>();
        services.AddSingleton<ProductHelper>();
    }
}