using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.HelperServices;
using Marketplace.Services.Products.Interfaces;
using Marketplace.Services.Products.Models.CategoryModels;
using Marketplace.Services.Products.Models.CategoryModels.CategoryViewModel;
using MongoDB.Driver;

namespace Marketplace.Services.Products.Managers;

public class CategoryManager : ICategoryManager
{
    private readonly CategoryHelper _categoryHelper;
    private readonly IMongoCollection<Category> _categoryCollection;
    public CategoryManager(IConfiguration configuration, CategoryHelper categoryHelper)
    {
        _categoryHelper = categoryHelper;
        var connectionString = configuration.GetConnectionString("ConnectionToMongoDb");

        var client = new MongoClient(connectionString);
        var database = client.GetDatabase("products");
        _categoryCollection = database.GetCollection<Category>("categories");
    }

    public async Task<CategoryViewModel> AddCategoryAsync(CreateCategoryModel model)
    {
        var keyCategory = _categoryHelper.GenerateKey(model.CategoryName);

        var isExists = await  _categoryHelper
            .CheckUniqueCategoryNameAndKey(_categoryCollection, model.CategoryName, keyCategory);

        if (isExists)
        {
            throw new Exception("This category name already exists!");
        }

        var newCategory = new Category
        {
            Id = Guid.NewGuid(),
            CategoryName = model.CategoryName,
            KeyCategory = keyCategory,
            ParentId = model.ParentId,
        };

        await _categoryCollection.InsertOneAsync(newCategory);

        return _categoryHelper.ConvertToCategoryViewModel(newCategory);
    }


    public async Task<CategoryViewModel> UpdateCategoryAsync(Guid categoryId, UpdateCategoryModel model)
    {
        var category = await _categoryHelper.FindByIdOrNameAsync(_categoryCollection, categoryId: categoryId);

        if (category is null)
            throw new Exception(message: "Category not found");

        var filter = Builders<Category>.Filter.Eq(c => c.Id, categoryId);


        category.ParentId = model.ParentId ?? category.ParentId;

        if (model.CategoryName != null)
        {
            category.CategoryName = model.CategoryName;
            category.KeyCategory = _categoryHelper.GenerateKey(model.CategoryName);
        }

        await _categoryCollection.ReplaceOneAsync(filter, category);

        return _categoryHelper.ConvertToCategoryViewModel(category);
    }

    
    public async Task DeleteAsync(Guid categoryId)
    {
        await _categoryCollection.DeleteOneAsync(c => c.Id == categoryId);
    }

    public async Task<List<CategoryWithChildModel>> GetCategoriesWithChildAsync()
    {
        var categories = await _categoryHelper.GetAllCategoriesAsync(_categoryCollection);

        return _categoryHelper.MapToCategoryModels(categories);
    }

    public async Task<List<Category>> GetCategoriesAsync()
    {
        return await _categoryHelper.GetAllCategoriesAsync(_categoryCollection);
    }

    public async Task<CategoryWithChildModel> GetCategoryByNameAsync(string categoryName)
    {
        return await _categoryHelper.GetCategoryWithChildAsync(_categoryCollection, categoryName: categoryName);
    }

    public async Task<CategoryWithChildModel> GetCategoryByIdAsync(Guid categoryId)
    {
        return await _categoryHelper.GetCategoryWithChildAsync(_categoryCollection, categoryId: categoryId);
    }


  
}