using Ecommerce.API.Application.DTOs.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Commands.Subcategory;

public class CreateSubcategoryCommand : IRequest<ReadSubcategoryDTO>
{
    public string? Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
}
