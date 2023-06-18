namespace Marketplace.Services.Products.Models.CategoryModels;

public class UpdateCategoryModel
{
    public Guid? ParentId { get; set; }
    public string? CategoryName { get; set; }
}