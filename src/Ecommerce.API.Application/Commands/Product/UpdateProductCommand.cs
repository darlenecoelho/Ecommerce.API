using Ecommerce.API.Application.Responses.Product;
using MediatR;

namespace Ecommerce.API.Application.Commands.Product;

public class UpdateProductCommand : IRequest<UpdateProductResponse>
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public bool Status { get; set; }
    public int CategoryId { get; set; }
    public int SubcategoryId { get; set; }
}