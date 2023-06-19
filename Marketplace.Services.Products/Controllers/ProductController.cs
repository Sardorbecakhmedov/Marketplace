using Marketplace.Services.Products.Interfaces;
using Marketplace.Services.Products.Models.ProductModels;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ProductController : ControllerBase
{
    private readonly IProductManager _productManager;

    public ProductController(IProductManager productManager)
    {
        _productManager = productManager;
    }

    [HttpPost("[action]")]
    public async Task<IActionResult> AddProduct(CreateProductModel model)
    {
        var product = await _productManager.AddProductAsync(model);
        return Ok(product);
    }


    [HttpPost("[action]")]
    public async Task<IActionResult> SaveProductImages(Guid productId, List<IFormFile> files)
    {
        var productImages = await _productManager.SaveImage(productId, files);
        return Ok(productImages);
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllProductsWithImages()
    {
        return Ok(await _productManager.GetProductsWithImages());
    }


    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllProducts()
    {
        return Ok(await _productManager.GetAllProductsAsync());
    }

    [HttpGet("[action]")]
    public async Task<IActionResult> GetAllImages()
    {
        return Ok(await _productManager.GetAllImagesAsync());
    }
}