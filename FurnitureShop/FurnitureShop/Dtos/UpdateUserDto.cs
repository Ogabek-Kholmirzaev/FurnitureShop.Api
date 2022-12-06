using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Dtos;

public class UpdateUserDto
{
    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
}