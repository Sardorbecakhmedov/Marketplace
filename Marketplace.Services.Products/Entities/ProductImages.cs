using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Services.Products.Entities;

public class ProductImages
{
    [BsonId]
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public string? ImagePath { get; set; }
}