using Ecommerce.API.Application.Responses.Category;
using MediatR;

namespace Ecommerce.API.Application.Commands.Category;

public class DeleteCategoryCommand : IRequest<DeleteCategoryResponse>
{
    public int Id { get; set; }
}
