using Ecommerce.API.Application.DTOs.Product;
using MediatR;

namespace Ecommerce.API.Application.Queries.Product;

public class GetProductByIdQuery : IRequest<ReadProductDTO>
{
    public int Id { get; set; }
}
