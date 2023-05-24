using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.DTOs.Subcategory;

namespace Ecommerce.API.Application.DTOs.Product;
public class ReadProductDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public ReadCategoryDTO Category { get; set; }
    public ReadSubcategoryDTO Subcategory { get; set; }
}
