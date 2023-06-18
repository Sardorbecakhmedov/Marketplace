namespace Marketplace.Services.Products.Models.CategoryModels;

public class CreateCategoryModel
{
    public Guid? ParentId { get; set; }
    public required string CategoryName { get; set; }
}