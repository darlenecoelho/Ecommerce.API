using Ecommerce.API.Application.Responses.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Commands.Subcategory;

public class DeleteSubcategoryCommand : IRequest<DeleteSubcategoryResponse>
{
    public int Id { get; set; }
}
