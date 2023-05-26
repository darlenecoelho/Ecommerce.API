namespace Ecommerce.API.Application.DTOs.Subcategory
{
    public class UpdateSubcategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public bool Status { get; set; }
        public int CategoryId { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
