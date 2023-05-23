namespace Ecommerce.API.Domain.Entities;
public class Subcategory
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
    public Category Category { get; set; }
}
