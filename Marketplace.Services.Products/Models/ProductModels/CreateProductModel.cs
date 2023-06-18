using Marketplace.Services.Products.Entities;

namespace Marketplace.Services.Products.Models.ProductModels;

public class CreateProductModel
{
    public required string CategoryId { get; set; }

    public required string ProductName { get; set; }
    public required string Description { get; set; }


    public Dictionary<string, object>? Characteristics { get; set; }
    public List<ProductImages>? Images { get; set; }
}