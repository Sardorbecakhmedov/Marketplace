using Marketplace.Services.Products.Interfaces;
using Marketplace.Services.Products.Models.CategoryModels;
using Microsoft.AspNetCore.Mvc;

namespace Marketplace.Services.Products.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryManager _categoryManager;

    public CategoryController(ICategoryManager categoryManager)
    {
        _categoryManager = categoryManager;
    }


    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromForm] CreateCategoryModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        return Ok(await _categoryManager.AddCategoryAsync(model));
    }

    [HttpGet]
    public async Task<IActionResult> GetCategoriesWithChild()
    {
        return Ok(await _categoryManager.GetCategoriesWithChildAsync());
    }

    [HttpGet("GetAllCategories")]
    public async Task<IActionResult> GetAllCategories()
    {
        return Ok(await _categoryManager.GetCategoriesAsync());
    }

    [HttpGet("{categoryId}")]
    public async Task<IActionResult> GetCategoryById(Guid categoryId)
    {
        return Ok(await _categoryManager.GetCategoryByIdAsync(categoryId));
    }

    [HttpGet("GetCategoryByName")]
    public async Task<IActionResult> GetCategoryByName(string categoryName)
    {
        return Ok(await _categoryManager.GetCategoryByNameAsync(categoryName));
    }

    [HttpPut("{categoryId}")]
    public async Task<IActionResult> UpdateCategory(Guid categoryId, [FromForm] UpdateCategoryModel model)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        return Ok(await _categoryManager.UpdateCategoryAsync(categoryId, model));
    }

    [HttpDelete("{categoryId}")]
    public async Task<IActionResult> GetAllCategories(Guid categoryId)
    {
        await _categoryManager.DeleteAsync(categoryId);
        return Ok();
    }
}