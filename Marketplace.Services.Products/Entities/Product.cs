using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Services.Products.Entities;

public class Product
{
    [BsonId]
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;

    public string CategoryId { get; set; } = null!;

    public Dictionary<string, object>? Characteristics { get; set; }
    public List<ProductImages>? Images { get; set; }
}