using FurnitureShop.Dtos;
using FurnitureShop.ViewModel;

namespace FurnitureShop.Services;

public interface ICategoriesService
{
    Task<List<CategoryView>> GetCategoriesAsync();
    Task<CategoryView> GetCategoryByIdAsync(int categoryId);
    Task AddCategory(CreateCategoryDto createCategoryDto);
    Task UpdateCategory(int categoryId, UpdateCategoryDto updateCategoryDto);
    Task DeleteCategory(int categoryId);
}