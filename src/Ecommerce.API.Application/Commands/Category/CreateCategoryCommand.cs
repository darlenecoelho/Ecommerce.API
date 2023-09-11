using Ecommerce.API.Application.Responses.Category;
using MediatR;

namespace Ecommerce.API.Application.Commands.Category;

public class CreateCategoryCommand : IRequest<CreateCategoryResponse>
{
    public string? Name { get; set; }
    public bool Status { get; set; }
    public DateTime DateRegister { get; set; }

}