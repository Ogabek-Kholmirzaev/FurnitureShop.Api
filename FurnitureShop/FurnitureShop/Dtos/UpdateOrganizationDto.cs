using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Dtos;

public class UpdateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}