﻿namespace Ecommerce.API.Application.DTOs.Subcategory;
public class CreateSubcategoryDTO
{
    public string? Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
    public DateTime DateRegister { get; set; }
    public DateTime LastUpdate { get; set; }
}
