using Ecommerce.API.Application.DTOs.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Queries.Subcategory;

public class GetSubcategoryByIdQuery : IRequest<ReadSubcategoryDTO>
{
    public int Id { get; set; }
}
