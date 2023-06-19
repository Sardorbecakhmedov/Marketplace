using Marketplace.Services.Products.Entities;
using Marketplace.Services.Products.HelperServices;
using Marketplace.Services.Products.Interfaces;
using Marketplace.Services.Products.Models.ProductModels;
using MongoDB.Driver;

namespace Marketplace.Services.Products.Managers;

public class ProductManager : IProductManager
{
    private readonly ProductHelper _productHelper;
    private readonly IMongoCollection<Product> _productCollection;
    private readonly IMongoCollection<Image> _imageCollection;
    public ProductManager(IConfiguration configuration, ProductHelper productHelper)
    {
        _productHelper = productHelper;
        var connectionString = configuration.GetConnectionString("ConnectionToMongoDb");

        var mongoClient = new MongoClient(connectionString);
        var database = mongoClient.GetDatabase("products");
        _productCollection = database.GetCollection<Product>("products");
        _imageCollection = database.GetCollection<Image>("images");
    }

    public async Task<Product> AddProductAsync(CreateProductModel model)
    {
        var newProduct = new Product
        {
            Id = Guid.NewGuid(),
            CategoryId = model.CategoryId,
            OrganizationId = model.OrganizationId,
            ProductName = model.ProductName,
            Description = model.Description,
            Price = model.Price,
            Characteristics = model.Characteristics
        };

        await _productCollection.InsertOneAsync(newProduct);

        return newProduct;
    }

    public async Task<List<ProductModel>> GetProductsWithImages()
    {
        var products = await GetAllProductsAsync();
        var images = await GetAllImagesAsync();

        var productModels = new List<ProductModel>();

        foreach (var product in products)
        {
            var productImages = _productHelper.GetProductImages(product.Id, images);

            var productModel = new ProductModel
            {
                Id = product.Id,
                CategoryId = product.CategoryId,
                OrganizationId = product.OrganizationId,
                ProductName = product.ProductName,
                Description = product.Description,
                Price = product.Price,
                Characteristics = product.Characteristics,
                ImagePaths = productImages
            };

            productModels.Add(productModel);
        }

        return productModels;
    }



    public async Task<List<string>> SaveImage(Guid productId, List<IFormFile> images)
    {
        var product = await _productHelper.FindByIdAsync(_productCollection, productId);

        if (product is null)
            throw new Exception("Product not found!");
        
        var imagesPaths = await _productHelper.SaveProductImagesAsync(images);

        foreach (var item in imagesPaths)
        {
            var image = new Image
            {
                Id = Guid.NewGuid(),
                ImagePath = item,
                ProductId = productId
            };

            await _imageCollection.InsertOneAsync(image);
        }

        return imagesPaths;
    }


    public async Task<List<Image>> GetAllImagesAsync()
    {
        return await (await _imageCollection.FindAsync(_ => true)).ToListAsync();
    }
    public async Task<List<Product>> GetAllProductsAsync()
    {
        return await (await _productCollection.FindAsync(_ => true)).ToListAsync();
    }


}