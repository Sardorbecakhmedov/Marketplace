namespace Marketplace.Services.Products.Models.CategoryModels;

public class CategoryViewModel
{
    public Guid Id { get; set; }

    public string CategoryName { get; set; } = null!;
    public string KeyCategory { get; set; } = null!;
    public Guid? ParentId { get; set; }
}