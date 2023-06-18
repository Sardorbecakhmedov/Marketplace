using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models.CategoryModels;
using Marketplace.Services.Products.Models.ProductModels;

namespace Marketplace.Services.Products.Interfaces;

public interface IProductManager
{
    Task<CreateProductModel> AddProductAsync(CreateCategoryModel model);
    Task<CategoryViewModel> UpdateCategoryAsync(Guid categoryId, UpdateCategoryModel model);
    Task DeleteAsync(Guid categoryId);
    Task<List<CategoryWithChildModel>> GetCategoriesWithChildAsync();
    Task<List<Category>> GetCategoriesAsync();
    Task<CategoryWithChildModel> GetCategoryByNameAsync(string categoryName);
    Task<CategoryWithChildModel> GetCategoryByIdAsync(Guid categoryId);
}