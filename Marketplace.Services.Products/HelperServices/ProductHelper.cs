using Marketplace.Common.Interfaces;
using Marketplace.Services.Products.Entities;
using MongoDB.Driver;

namespace Marketplace.Services.Products.HelperServices;

public class ProductHelper
{
    private readonly IFileManager _fileManager;

    public ProductHelper(IFileManager fileManager)
    {
        _fileManager = fileManager;
    }

    public List<string> GetProductImages(Guid productId, List<Image> allImages)
    {
        var productImages = allImages.Where(i => i.ProductId == productId);

        return (from image in productImages
                where image.ImagePath != null
                select image.ImagePath)
                .ToList();
    }

    public async Task<List<string>> SaveProductImagesAsync(List<IFormFile> images)
    {
        var productImages = new List<string>();

        foreach (var image in images)
        {
            var imagePath = await _fileManager.SaveFileToWwwrootAsync(image, "Images");
            productImages.Add(imagePath);
        }

        return productImages;
    }

    public async Task<Product?> FindByIdAsync(IMongoCollection<Product> productCollection,
        Guid productId)
    {

        return await (await productCollection.FindAsync(c => c.Id == productId))
            .FirstOrDefaultAsync();
    }
}