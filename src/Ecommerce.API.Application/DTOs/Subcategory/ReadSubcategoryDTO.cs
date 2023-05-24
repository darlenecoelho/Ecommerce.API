using Ecommerce.API.Application.DTOs.Category;
using Ecommerce.API.Application.DTOs.Product;

namespace Ecommerce.API.Application.DTOs.Subcategory;
public class ReadSubcategoryDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
    public ReadCategoryDTO Category { get; set; }
    public IList<ReadProductDTO> Products { get; set; } = new List<ReadProductDTO>();

}
