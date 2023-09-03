using Ecommerce.API.Application.Commands.Subcategory;
using Ecommerce.API.Application.DTOs.Subcategory;
using Ecommerce.API.Application.Queries.Subcategory;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Ecommerce.API.Controllers;

[ApiController]
[Route("api/subcategoriesCQRS")]
public class SubcategoryControllerCQRS : ControllerBase
{
    private readonly IMediator _mediator;

    public SubcategoryControllerCQRS(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<ReadSubcategoryDTO>), 200)]
    public async Task<IActionResult> GetAllSubcategoriesAsync()
    {
        var query = new GetAllSubcategoriesQuery();
        var result = await _mediator.Send(query);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(ReadSubcategoryDTO), 201)]
    [ProducesResponseType(typeof(string), 400)]
    public async Task<IActionResult> CreateSubcategoryAsync([FromBody] CreateSubcategoryCommand command)
    {
        var result = await _mediator.Send(command);
        return Ok(result);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ReadSubcategoryDTO), 200)]
    public async Task<IActionResult> GetSubcategoryByIdAsync(int id)
    {
        var query = new GetSubcategoryByIdQuery { Id = id };
        var result = await _mediator.Send(query);
        return Ok(result);
    }
}

