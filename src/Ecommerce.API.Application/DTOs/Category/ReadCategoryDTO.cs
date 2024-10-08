﻿using Ecommerce.API.Application.DTOs.Subcategory;

namespace Ecommerce.API.Application.DTOs.Category
{
    public class ReadCategoryDTO
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public DateTime DateRegister { get; set; }
        public DateTime LastUpdate { get; set; }
        public bool Status { get; set; }
        public IList<ReadSubcategoryDTO> Subcategories { get; set; } = new List<ReadSubcategoryDTO>();
    }
}
