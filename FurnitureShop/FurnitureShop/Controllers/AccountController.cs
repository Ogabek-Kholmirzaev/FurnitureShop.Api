using FurnitureShop.Data;
using FurnitureShop.Dtos;
using FurnitureShop.Entities;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AccountController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountController(AppDbContext context, UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _context = context;
        _userManager = userManager;
        _signInManager = signInManager;
    }

    [HttpPost("signup")]
    public async Task<IActionResult> SignUp(RegisterUserDto registerUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState);

        var user = registerUserDto.Adapt<AppUser>();

        var result = await _userManager.CreateAsync(user, registerUserDto.Password);

        if (!result.Succeeded)
        {
            return BadRequest(result);
        }

        await _signInManager.SignInAsync(user, isPersistent: true);

        return Ok();
    }

    [HttpPost("signin")]
    public async Task<IActionResult> SignIn(LoginUserDto loginUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = loginUserDto.Adapt<AppUser>();

        var result = await _signInManager.PasswordSignInAsync(user.UserName, loginUserDto.Password, true, true);

        if(!result.Succeeded)
            return BadRequest(result);

        return Ok();
    }
}
