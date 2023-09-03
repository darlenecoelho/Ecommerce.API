namespace Ecommerce.API.Application.DTOs.Category
{
    public class UpdateCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
    }
}
