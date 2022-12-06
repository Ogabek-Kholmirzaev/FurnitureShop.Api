using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Dtos;

public class UpdateCategoryDto
{
    [Required]
    public string? Name { get; set; }
    public int? ParentId { get; set; }
}