using Microsoft.AspNetCore.Identity;

namespace FurnitureShop.Entities;

public class AppUser:IdentityUser<Guid>
{
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public EUserStatus Status { get; set; }
    public virtual ICollection<OrganizationUser>? Organizations { get; set; }
}