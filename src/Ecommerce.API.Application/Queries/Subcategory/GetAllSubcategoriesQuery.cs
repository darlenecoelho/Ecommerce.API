using Ecommerce.API.Application.DTOs.Subcategory;
using MediatR;

namespace Ecommerce.API.Application.Queries.Subcategory;

public class GetAllSubcategoriesQuery : IRequest<List<ReadSubcategoryDTO>>
{
}
