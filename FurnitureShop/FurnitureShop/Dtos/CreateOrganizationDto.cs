using System.ComponentModel.DataAnnotations;

namespace FurnitureShop.Dtos;

public class CreateOrganizationDto
{
    [Required]
    public string? Name { get; set; }
}