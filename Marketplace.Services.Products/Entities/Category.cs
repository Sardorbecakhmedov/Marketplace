using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Services.Products.Entities;

public class Category
{
    [BsonId]
    public Guid Id { get; set; } 

    public string CategoryName { get; set; } = null!;
    public string KeyCategory { get; set; } = null!;
    public Guid? ParentId { get; set; }

}