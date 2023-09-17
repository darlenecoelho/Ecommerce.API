using Ecommerce.API.Application.Responses.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Commands.Subcategory;

public class UpdateSubcategoryCommand : IRequest<UpdateSubcategoryResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }

}
