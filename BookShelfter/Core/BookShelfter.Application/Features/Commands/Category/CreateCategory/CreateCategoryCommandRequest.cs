using MediatR;

namespace BookShelfter.Application.Features.Commands.Category;

public class CreateCategoryCommandRequest:IRequest<CreateCategoryCommandResponse>
{
    public string  CategoryName { get; set; }


    
    
    
}