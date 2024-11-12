using BookShelfter.Application.Repositories.Category;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Category.RemoveCategory;

public class RemoveCategoryCommandHandler:IRequestHandler<RemoveCategoryCommandRequest,RemoveCategoryCommandResponse>
{
    private readonly ICategoryWriteRepository _categoryWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public RemoveCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository, ICategoryReadRepository categoryReadRepository)
    {
        _categoryWriteRepository = categoryWriteRepository;
        _categoryReadRepository = categoryReadRepository;
    }

    public async  Task<RemoveCategoryCommandResponse> Handle(RemoveCategoryCommandRequest request, CancellationToken cancellationToken)
    {
     // var result= await _categoryWriteRepository.RemoveAsync(request.CategoryId);
     //    await _categoryWriteRepository.SaveAsync();
     //    return new(){};
        try
        {
            var category = await  _categoryReadRepository.GetByIdAsync(request.CategoryId);
            if (category==null)
            {
                return new RemoveCategoryCommandResponse()
                {
                    Success = false,
                    Message = "Category not found"
                };
        
            }

            var result =await  _categoryWriteRepository.RemoveAsync(category.Id.ToString());
        
           await _categoryWriteRepository.SaveAsync();
           
           
        
           return new()
           {
               Success = true,
               Message = "Category remove successfully"
           };
        
        
        
        }
        catch (Exception  ex)
        {
            return new()
            {
                Success = false,
                Message = "category could not be  deleted succesfully",
                Errors = new List<string>{ex.Message}
            };
        
        
        }
     
    }
}