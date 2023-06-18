using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models.CategoryModels;

namespace Marketplace.Services.Products.Interfaces;

public interface ICategoryManager
{
    Task<CategoryViewModel> AddCategoryAsync(CreateCategoryModel model);
    Task<CategoryViewModel> UpdateCategoryAsync(Guid categoryId, UpdateCategoryModel model);
    Task DeleteAsync(Guid categoryId);
    Task<List<CategoryWithChildModel>> GetCategoriesWithChildAsync();
    Task<List<Category>> GetCategoriesAsync();
    Task<CategoryWithChildModel> GetCategoryByNameAsync(string categoryName);
    Task<CategoryWithChildModel> GetCategoryByIdAsync(Guid categoryId);
}