using System.Security.Claims;
using FurnitureShop.Data;
using FurnitureShop.Dtos;
using FurnitureShop.Entities;
using FurnitureShop.Exceptions;
using FurnitureShop.ViewModel;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace FurnitureShop.Services;

public class OrganizationService : IOrganizationService
{
    private readonly AppDbContext _appDbContext;

    public OrganizationService(AppDbContext appDbContext)
    {
        _appDbContext = appDbContext;
    }

    public async Task<List<OrganizationView>> GetOrganizationsAsync()
    {
        return (await _appDbContext.Organizations.ToListAsync()).Adapt<List<OrganizationView>>();
    }

    public async Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId)
    {
        var organization = await _appDbContext.Organizations.FindAsync(organizationId);

        if (organization == null)
            throw new OrganizationNotFoundException();

        return organization.Adapt<OrganizationView>();
    }

    public async Task AddOrganization(ClaimsPrincipal claims, CreateOrganizationDto createOrganizationDto)
    {
        var organization = createOrganizationDto.Adapt<Organization>();

        var userId = Guid.Parse(claims.FindFirst(ClaimTypes.NameIdentifier)!.Value);

        organization.Users = new List<OrganizationUser>
        {
            new OrganizationUser()
            {
                Role = ERole.Owner,
                UserId = userId
            }
        };

        _appDbContext.Organizations.Add(organization);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto)
    {
        var organization = await _appDbContext.Organizations.FindAsync(organizationId);

        if (organization == null)
            throw new OrganizationNotFoundException();

        organization.Name = updateOrganizationDto.Name;

        _appDbContext.Update(organization);
        await _appDbContext.SaveChangesAsync();
    }

    public async Task DeleteOrganization(Guid organizationId)
    {
        var organization = await _appDbContext.Organizations.FindAsync(organizationId);

        if (organization == null)
            throw new OrganizationNotFoundException();

        _appDbContext.Remove(organization);
        await _appDbContext.SaveChangesAsync();
    }
}