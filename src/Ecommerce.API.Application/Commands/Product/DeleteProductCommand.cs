using Ecommerce.API.Application.Responses.Product;
using MediatR;

namespace Ecommerce.API.Application.Commands.Product;

public class DeleteProductCommand : IRequest<DeleteProductResponse>
{
    public int Id { get; set; }
}