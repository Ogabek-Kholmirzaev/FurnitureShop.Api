namespace FurnitureShop.Entities;

public class Product
{
    public Guid Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public Dictionary<string, string>? Properties { get; set; }
    public decimal Price { get; set; }

    public virtual List<ProductImage>? Images { get; set; }
}