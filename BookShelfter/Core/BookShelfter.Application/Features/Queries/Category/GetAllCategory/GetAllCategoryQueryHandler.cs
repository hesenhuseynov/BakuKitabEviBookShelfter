using BookShelfter.Application.Repositories.Category;
using BookShelfter.Application.ViewModels.Category;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace BookShelfter.Application.Features.Queries.Category.GetAllCategory;

public class GetAllCategoryQueryHandler:IRequestHandler<GetAllCategoryQueryRequest,GetAllCategoryQueryResponse>
{
    private readonly ICategoryReadRepository _categoryReadRepository;

    public GetAllCategoryQueryHandler(ICategoryReadRepository categoryReadRepository)
    {
        _categoryReadRepository = categoryReadRepository;
    }

    public Task<GetAllCategoryQueryResponse> Handle(GetAllCategoryQueryRequest request, CancellationToken cancellationToken)
    { 
        
        
        var categories = _categoryReadRepository
            .GetAll()
            .Select(c => new VM_GetAllCategory()
            {

                CategoryId = c.Id,
                CategoryName = c.CategoryName

            })
            // .Include(c => c.Books) 
            .ToList();  
        
        var response = new GetAllCategoryQueryResponse()
        {
            TotalCount = categories.Count(),
            Category = categories,
            
            
         
        };
        
        
        return Task.FromResult(response);


    }
}