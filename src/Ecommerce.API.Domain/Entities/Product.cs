namespace Ecommerce.API.Domain.Entities;
public class Product
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string? Description { get; set; }
    private decimal _price;
    public decimal Price
    {
        get { return _price; }
        set { _price = Math.Round(value, 2); }
    }
    public int Stock { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public Category? Category { get; set; }
    public int CategoryId { get; set; }
    public Subcategory? Subcategory { get; set; }
    public int SubcategoryId { get; set; }
}
