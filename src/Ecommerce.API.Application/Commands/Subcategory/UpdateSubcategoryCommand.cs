using Ecommerce.API.Application.DTOs.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Commands.Subcategory;

public class UpdateSubcategoryCommand : IRequest<ReadSubcategoryDTO>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
    public DateTime LastUpdate { get; set; }
}
