namespace Marketplace.Services.Products.Models.ProductModels;

public class CreateImageModel
{
    public required Guid ProductId { get; set; }
    public required IFormFile Image { get; set; }
}