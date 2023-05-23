namespace Ecommerce.API.Domain.Entities;
public class Category
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
    public bool Status { get; set; }
    public IList<Subcategory> Subcategories { get; set; } = new List<Subcategory>();

}
