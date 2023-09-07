using MediatR;

namespace Ecommerce.API.Application.Commands.Category;

public class UpdateCategoryCommand 
{
    public int Id { get; set; }
    public string? NewName { get; set; }
}
