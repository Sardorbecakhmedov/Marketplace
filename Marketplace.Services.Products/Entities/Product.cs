﻿using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Marketplace.Services.Products.Entities;

public class Product
{
    [BsonId]
    public Guid Id { get; set; }
    public string ProductName { get; set; } = null!;
    public string Description { get; set; } = null!;
    public decimal Price { get; set; }
    public string CategoryId { get; set; } = null!;
    public string OrganizationId { get; set; } = null!;

    public Dictionary<string, string>? Characteristics { get; set; }
}