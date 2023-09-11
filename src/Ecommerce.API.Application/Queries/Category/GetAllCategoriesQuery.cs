using Ecommerce.API.Application.DTOs.Category;
using MediatR;

namespace Ecommerce.API.Application.Queries.Category;

public class GetAllCategoriesQuery : IRequest<List<ReadCategoryDTO>>
{
}
