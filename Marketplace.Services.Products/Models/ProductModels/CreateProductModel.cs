namespace Marketplace.Services.Products.Models.ProductModels;

public class CreateProductModel
{
    public required string CategoryId { get; set; }
    public required string OrganizationId { get; set; }
    public required string ProductName { get; set; }
    public required string Description { get; set; }
    public decimal Price { get; set; }

    public Dictionary<string, string>? Characteristics { get; set; }
}