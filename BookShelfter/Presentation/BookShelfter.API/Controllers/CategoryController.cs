using System.Net;
using BookShelfter.Application.Features.Commands.Category;
using BookShelfter.Application.Features.Commands.Category.RemoveCategory;
using BookShelfter.Application.Features.Queries.Category.GetAllCategory;
using BookShelfter.Application.Features.Queries.Category.GetBooksByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookShelfter.API.Controllers;



[Route("api/[controller]")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly IMediator _mediator;

    public CategoryController(IMediator mediator)
    {

        _mediator = mediator;

    }

    [HttpPost]
    public async Task<IActionResult> Post(CreateCategoryCommandRequest createCategoryCommandRequest)
    {
        CreateCategoryCommandResponse response = await _mediator.Send(createCategoryCommandRequest);


        if (response.Success)
        {
            return StatusCode((int)HttpStatusCode.Created);


        }

        return BadRequest(new { errors = response.Errors });

    }


    [HttpGet("GetAll")]
    public async Task<IActionResult> GetAll([FromQuery] GetAllCategoryQueryRequest request)
    {
        try
        {
            GetAllCategoryQueryResponse response = await _mediator.Send(request);
                
            return Ok(response);


        }

        catch (Exception e)
        {
            return StatusCode(500, "Internal Server error");
        }

    }



    [HttpDelete("{CategoryId}")]
    public async Task<IActionResult> RemoveCategoryById([FromRoute] RemoveCategoryCommandRequest request)
    {
        RemoveCategoryCommandResponse response = await _mediator.Send(request);
        if (response.Success)
        {
            return Ok(response.Message);
        }
        return BadRequest(response.Message);

    }
    


    [HttpGet("{CategoryId}/books")]
    public async Task<IActionResult> GetBookByCategories([FromRoute] GetBooksByCategoryQueryRequest categoryQueryRequest)
    {
        GetBooksByCategoryQueryResponse response = await _mediator.Send(categoryQueryRequest);

        

        if (response.Success)
        {
            return Ok(response);

        }
        
        return BadRequest(response.Message);

    }



}