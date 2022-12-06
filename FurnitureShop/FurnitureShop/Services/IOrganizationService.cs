using System.Security.Claims;
using FurnitureShop.Dtos;
using FurnitureShop.ViewModel;

namespace FurnitureShop.Services;

public interface IOrganizationService
{
    Task<List<OrganizationView>> GetOrganizationsAsync();
    Task<OrganizationView> GetOrganizationByIdAsync(Guid organizationId);
    Task AddOrganization(ClaimsPrincipal claims, CreateOrganizationDto createOrganizationDto);
    Task UpdateOrganization(Guid organizationId, UpdateOrganizationDto updateOrganizationDto);
    Task DeleteOrganization(Guid organizationId);
}