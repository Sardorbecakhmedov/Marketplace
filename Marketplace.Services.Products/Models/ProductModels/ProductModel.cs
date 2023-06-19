namespace Marketplace.Services.Products.Models.ProductModels;

public class ProductModel
{
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string CategoryId { get; set; } = null!;
    public string OrganizationId { get; set; } = null!;

    public Dictionary<string, string>? Characteristics { get; set; }
    public List<string>? ImagePaths { get; set; }
}