using FurnitureShop.Data;
using FurnitureShop.Dtos;
using FurnitureShop.Entities;
using FurnitureShop.Exceptions;
using FurnitureShop.ViewModel;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Services;

public class CategoriesService : ICategoriesService
{
    private readonly AppDbContext _appDbContext;

    public CategoriesService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<CategoryView>> GetCategoriesAsync()
    {
        var categories = await _appDbContext.Categories.ToListAsync();

        var categoriesView = new List<CategoryView>();

        foreach (var category in categories)
        {
            var categoryView = category.Adapt<CategoryView>();

            categoryView.Children = GetCategoryChildren(category.Id);

            categoriesView.Add(categoryView);
        }

        return categoriesView;
    }

    public async Task<CategoryView> GetCategoryByIdAsync(int categoryId)
    {
        var category = await _appDbContext.Categories.FindAsync(categoryId);

        if (category == null)
            throw new CategoryNotFoundException();

        var categoryView = category.Adapt<CategoryView>();

        categoryView.Children = GetCategoryChildren(category.Id);

        return categoryView;
    }

    public async Task AddCategory(CreateCategoryDto createCategoryDto)
    {
        var category = createCategoryDto.Adapt<Category>();

        _appDbContext.Categories.Add(category);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto)
    {
        var category = await _appDbContext.Categories.FindAsync(categoryId);

        if (category == null)
            throw new CategoryNotFoundException();

        category.Name = updateCategoryDto.Name;
        category.ParentId = updateCategoryDto.ParentId;

        _appDbContext.Update(category);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteCategory(int categoryId)
    {
        var category = await _appDbContext.Categories.FindAsync(categoryId);

        if (category == null)
            throw new CategoryNotFoundException();

        _appDbContext.Remove(category);
        await _appDbContext.SaveChangesAsync();
    }

    public List<CategoryView>? GetCategoryChildren(int categoryId)
    {
        var categoriesView = new List<CategoryView>();

        var categories = _appDbContext.Categories.Where(c => c.ParentId == categoryId).ToList();

        /*if (categories == null)
            return null;*/

        foreach (var category in categories)
        {
            var categoryView = category.Adapt<CategoryView>();

            categoryView.Children = GetCategoryChildren(category.Id);

            categoriesView.Add(categoryView);
        }

        return categoriesView;
    }
}