using System.Text.RegularExpressions;
using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.Models.CategoryModels;
using MongoDB.Driver;

namespace Marketplace.Services.Products.HelperServices;

public class CategoryHelper
{

    public async Task<CategoryWithChildModel> GetCategoryWithChildAsync(IMongoCollection<Category> categoryCollection,
        Guid? categoryId = null, string? categoryName = null)
    {
        Category? category = null;

        if (categoryId != null)
            category = await FindByIdOrNameAsync(categoryCollection, categoryId: categoryId);
        else if (categoryName != null)
            category = await FindByIdOrNameAsync(categoryCollection, categoryName: categoryName);

        var categories = await GetAllCategoriesAsync(categoryCollection);

        if (category == null)
            throw new Exception("Category not found!");

        return CreateCategoryModel(category, categories);
    }

    public async Task<Category?> FindByIdOrNameAsync(IMongoCollection<Category> categoryCollection, 
        Guid? categoryId = null, string? categoryName = null)
    {
        if (categoryId != null)
        {
            return await (await categoryCollection.FindAsync(c => c.Id == categoryId))
                .FirstOrDefaultAsync();
        }

        return await (await categoryCollection.FindAsync(c => c.CategoryName == categoryName))
            .FirstOrDefaultAsync();
    }

    public async Task<List<Category>> GetAllCategoriesAsync(IMongoCollection<Category> categoryCollection)
    {
        return await (await categoryCollection.FindAsync(_ => true)).ToListAsync();
    }

    public async Task<bool> CheckUniqueCategoryNameAndKey(IMongoCollection<Category> categoryCollection, 
        string categoryName, string keyName)
    {
        var category = await categoryCollection
            .Find(n => n.CategoryName == categoryName || n.KeyCategory == keyName)
            .FirstOrDefaultAsync();

        return category != null;
    }
     
    public string GenerateKey(string valueName)
    {
        return Regex.Replace(valueName.ToLower(), @"\s+", "-");
    }

    public CategoryViewModel ConvertToCategoryViewModel(Category category)
    {
        return new CategoryViewModel
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            KeyCategory = category.KeyCategory,
            ParentId = category.ParentId
        };
    }

    public List<CategoryWithChildModel> MapToCategoryModels(List<Category> categories)
    {
        var parentCategories = categories.Where(p => p.ParentId == null);

        return parentCategories.Select(category => CreateCategoryModel(category, categories)).ToList();
    }

    public CategoryWithChildModel CreateCategoryModel(Category category, List<Category> categories)
    {
        var categoryModel = new CategoryWithChildModel
        {
            Id = category.Id,
            CategoryName = category.CategoryName,
            KeyCategory = category.KeyCategory,
            ChildCategories = new List<CategoryWithChildModel>()
        };

        var childCategories = categories.Where(parent => parent.ParentId == category.Id);

        foreach (var childCategory in childCategories)
        {
            var childCategoryModel = CreateCategoryModel(childCategory, categories);
            categoryModel.ChildCategories.Add(childCategoryModel);
        }

        return categoryModel;
    }
}