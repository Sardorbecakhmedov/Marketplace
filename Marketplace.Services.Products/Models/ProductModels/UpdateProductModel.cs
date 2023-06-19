namespace Marketplace.Services.Products.Models.ProductModels;

public class UpdateProductModel
{
    public required string CategoryId { get; set; } 

    public string? ProductName { get; set; } 
    public string? Description { get; set; }


    public Dictionary<string, object>? Characteristics { get; set; }
    public List<string>? Images { get; set; }
}