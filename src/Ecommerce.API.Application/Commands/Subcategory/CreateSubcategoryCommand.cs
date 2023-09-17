using Ecommerce.API.Application.Responses.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Commands.Subcategory;

public class CreateSubcategoryCommand : IRequest<CreateSubcategoryResponse>
{
    public string? Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
}
