using System.Net;
using BookShelfter.Application.Features.Commands.Category;
using BookShelfter.Application.Features.Commands.Category.RemoveCategory;
using BookShelfter.Application.Features.Queries.Category.GetAllCategory;
using BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;


using System.Net;
using BookShelfter.Application.Features.Commands.Category;
using BookShelfter.Application.Features.Commands.Category.RemoveCategory;
using BookShelfter.Application.Features.Queries.Category.GetAllCategory;
using BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize(AuthenticationSchemes = "Bearer")] 
public class CategoryController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<IActionResult> Post([FromBody] CreateCategoryCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? StatusCode((int)HttpStatusCode.Created, new { message = response.Message })
            : BadRequest(new { errors = response.Errors });
    }

    [AllowAnonymous]
    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return Ok(response);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("{CategoryId}")]
    public async Task<IActionResult> RemoveCategoryById([FromRoute] RemoveCategoryCommandRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(new { message = response.Message })
            : BadRequest(new { message = response.Message });
    }

    [AllowAnonymous]
    [HttpGet("{CategoryId}/books")]
    public async Task<IActionResult> GetBookByCategories([FromRoute] GetBooksByCategoryQueryRequest request)
    {
        var response = await _mediator.Send(request);
        return response.Success
            ? Ok(response)
            : BadRequest(new { message = response.Message });
    }
}
