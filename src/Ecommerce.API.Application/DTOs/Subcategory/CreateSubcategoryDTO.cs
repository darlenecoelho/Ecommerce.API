namespace Ecommerce.API.Application.DTOs;
public class CreateSubcategoryDTO
{
    public string Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
}
