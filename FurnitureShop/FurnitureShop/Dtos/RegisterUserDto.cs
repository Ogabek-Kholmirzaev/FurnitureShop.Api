using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Dtos;

public class RegisterUserDto
{
    [Required]
    public string? UserName { get; set; }

    [Required]
    public string? FirstName { get; set; }
    public string? LastName { get; set; }

    [Required]
    public string? Password { get; set; }
    
    [Required]
    public string? Email { get; set; }
}