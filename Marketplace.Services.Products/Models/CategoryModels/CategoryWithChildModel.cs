namespace Marketplace.Services.Products.Models.CategoryModels;

public class CategoryWithChildModel
{
    public Guid Id { get; set; }
    public string CategoryName { get; set; } = null!;
    public string KeyCategory { get; set; } = null!;
    public List<CategoryWithChildModel>? ChildCategories { get; set; } 
}