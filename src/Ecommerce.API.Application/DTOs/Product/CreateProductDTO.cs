namespace Ecommerce.API.Application.DTOs.Product;
public class CreateProductDTO
{
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public int CategoryId { get; set; }
    public int SubcategoryId { get; set; }
}
