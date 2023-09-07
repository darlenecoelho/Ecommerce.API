using Ecommerce.API.Application.DTOs.Category;
using MediatR;

namespace Ecommerce.API.Application.Queries.Category;

public class GetCategoryByIdQuery : IRequest<ReadCategoryDTO>
{
    public int Id { get; set; }

    public GetCategoryByIdQuery(int id)
    {
        Id = id;
    }
}
