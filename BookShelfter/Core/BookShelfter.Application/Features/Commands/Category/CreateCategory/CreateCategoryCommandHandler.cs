using BookShelfter.Application.Repositories.Category;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Category;

public class CreateCategoryCommandHandler:IRequestHandler<CreateCategoryCommandRequest,CreateCategoryCommandResponse>
{
    private readonly ICategoryWriteRepository _categoryWriteRepository;
    private readonly ICategoryReadRepository _categoryReadRepository;

    public CreateCategoryCommandHandler(ICategoryWriteRepository categoryWriteRepository,ICategoryReadRepository categoryReadRepository)
    {
        _categoryWriteRepository = categoryWriteRepository;
        _categoryReadRepository = categoryReadRepository;
    } 
    public async Task<CreateCategoryCommandResponse> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        try
        {

            var existingCategory =  await _categoryReadRepository.GetSingleAsync(c => c.CategoryName == request.CategoryName);
            
            if (existingCategory != null)
            {
                return new CreateCategoryCommandResponse
                {
                    Success = false,
                    Errors = new List<string> { "Category Name must be unique " }
                };
            }


            await _categoryWriteRepository.AddAsync(new Domain.Entities.Category(){
                CategoryName = request.CategoryName,
            });
            await _categoryWriteRepository.SaveAsync();

            return new()
            {
                Success = true,
            };

        }
        catch (Exception e)
        {
            return new()
            {
                Success = false,
                Errors = new List<string> { e.Message }
            };

        }








    }
}