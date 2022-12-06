using System.Runtime.CompilerServices;
using FurnitureShop.Dtos;
using FurnitureShop.Services;
using FurnitureShop.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesCantroller : ControllerBase
{
    private readonly ICategoriesService _categoriesService;

    public CategoriesCantroller(ICategoriesService categoriesService)
    {
        _categoriesService = categoriesService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<CategoryView>), StatusCodes.Status200OK)]
    public async Task<ActionResult> GetCategories() => Ok(await _categoriesService.GetCategoriesAsync());

    [HttpGet("{categoryId:int}")]
    [ProducesResponseType(typeof(CategoryView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetCategoryById(int categoryId) => Ok(await _categoriesService.GetCategoryByIdAsync(categoryId));

    [HttpPost]
    public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryDto createCategoryDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _categoriesService.AddCategory(createCategoryDto);

        return Ok();
    }

    [HttpPut("{categoryId:int}")]
    public async Task<IActionResult> UpdateCategory(int categoryId, [FromBody] UpdateCategoryDto updateCategoryDto)
    {
        if(!ModelState.IsValid)
            return BadRequest();

        await _categoriesService.UpdateCategory(categoryId, updateCategoryDto);

        return Ok();
    }

    [HttpDelete("{categoryId:int}")]
    public async Task<IActionResult> DeleteCategory(int categoryId)
    {
        await _categoriesService.DeleteCategory(categoryId);

        return Ok();
    }
}