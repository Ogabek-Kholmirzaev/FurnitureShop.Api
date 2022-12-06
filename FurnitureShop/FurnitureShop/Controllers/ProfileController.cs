using FurnitureShop.Dtos;
using FurnitureShop.Entities;
using FurnitureShop.ViewModel;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controllers;

[Route("api/[controller]")]
[ApiController]
[Authorize]
public class ProfileController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;

    public ProfileController(UserManager<AppUser> userManager)
    {
        _userManager = userManager;
    }

    [HttpGet]
    [ProducesResponseType(typeof(UserView), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetUserProfile()
    {
        var user = await _userManager.GetUserAsync(User);

        return Ok(user.Adapt<UserView>());
    }

    [HttpPut]
    public async Task<IActionResult> UpdateUserProfile([FromBody] UpdateUserDto updateUserDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        var user = await _userManager.GetUserAsync(User);

        user.FirstName = updateUserDto.FirstName;
        user.LastName = updateUserDto.LastName ?? user.LastName;

        await _userManager.UpdateAsync(user);

        return Ok();
    }
}