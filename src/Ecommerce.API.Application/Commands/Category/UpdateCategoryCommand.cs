using Ecommerce.API.Application.Responses.Category;
using MediatR;

namespace Ecommerce.API.Application.Commands.Category;

public class UpdateCategoryCommand : IRequest<UpdateCategoryResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public bool Status { get; set; }
}
