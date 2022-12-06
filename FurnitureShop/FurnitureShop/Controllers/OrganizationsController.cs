using FurnitureShop.Dtos;
using FurnitureShop.Services;
using FurnitureShop.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FurnitureShop.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrganizationsController : ControllerBase
{
    private readonly IOrganizationService _organizationService;

    public OrganizationsController(IOrganizationService organizationService)
    {
        _organizationService = organizationService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<OrganizationView>), StatusCodes.Status200OK)]
    public async Task<ActionResult<List<OrganizationView>>> GetOrganizations() =>
        await _organizationService.GetOrganizationsAsync();


    [HttpGet("{organizationId:guid}")]
    [ProducesResponseType(typeof(OrganizationView), StatusCodes.Status200OK)]
    public async Task<ActionResult<OrganizationView>> GetOrganizationById(Guid organizationId) =>
        await _organizationService.GetOrganizationByIdAsync(organizationId);

    [HttpPost]
    public async Task<IActionResult> CreateOrganization([FromBody] CreateOrganizationDto createOrganizationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _organizationService.AddOrganization(User, createOrganizationDto);

        return Ok();
    }

    [HttpPut("{organizationId:guid}")]
    public async Task<IActionResult> UpdateOrganization(Guid organizationId, [FromBody] UpdateOrganizationDto updateOrganizationDto)
    {
        if (!ModelState.IsValid)
            return BadRequest();

        await _organizationService.UpdateOrganization(organizationId, updateOrganizationDto);

        return Ok();
    }

    [HttpDelete("{organizationId:guid}")]
    public async Task<IActionResult> DeleteOrganization(Guid organizationId)
    {
        await _organizationService.DeleteOrganization(organizationId);
    
        return Ok();
    }
}