namespace Ecommerce.API.Domain.Entities
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public Subcategory Subcategory { get; set; }
        public int SubcategoryId { get; set; }
    }
}
