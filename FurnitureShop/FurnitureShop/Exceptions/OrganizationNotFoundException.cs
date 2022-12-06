namespace FurnitureShop.Exceptions;

public class OrganizationNotFoundException : Exception
{
    public OrganizationNotFoundException() : base("Organization not found") { }
}