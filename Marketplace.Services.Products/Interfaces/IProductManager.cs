using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models.ProductModels;

namespace Marketplace.Services.Products.Interfaces;

public interface IProductManager
{
    Task<Product> AddProductAsync(CreateProductModel model);
    Task<List<Product>> GetAllProductsAsync();
    Task<List<string>> SaveImage(Guid productId, List<IFormFile> images);
    Task<List<Image>> GetAllImagesAsync();
    Task<List<ProductModel>> GetProductsWithImages();
}