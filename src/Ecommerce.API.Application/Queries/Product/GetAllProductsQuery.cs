using Ecommerce.API.Application.DTOs.Product;
using MediatR;

namespace Ecommerce.API.Application.Queries.Product;

public class GetAllProductsQuery : IRequest<List<ReadProductDTO>>
{
}
