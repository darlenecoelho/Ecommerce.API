using Ecommerce.API.Application.DTOs.Category;
using MediatR;

namespace Ecommerce.API.Application.Queries.Category;

public class GetCategoryByNameQuery : IRequest<ReadCategoryDTO>
{
    public string? Name { get; set; }
}
