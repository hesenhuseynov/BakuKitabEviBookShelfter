using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.RemoveProduct;

public class RemoveProductCommandHandler:IRequestHandler<RemoveProductCommandRequest,RemoveProductCommandResponse>
{
    private readonly IBookWriteRepository _bookWriteRepository;

    private readonly IBookReadRepository _readRepository;

    public RemoveProductCommandHandler(IBookWriteRepository bookWriteRepository, IBookReadRepository readRepository)
    {
        _bookWriteRepository = bookWriteRepository;
        _readRepository = readRepository;
    }
    
    public async Task<RemoveProductCommandResponse> Handle(RemoveProductCommandRequest request, CancellationToken cancellationToken)
    {


        try
        {
            var result = await _bookWriteRepository.RemoveAsync(request.Id.ToString());

            if (!result)
            {
                return new()
                {
                    Success = false,
                    Message = "book not found or coult not be deleted"
                };
            }


            await _bookWriteRepository.SaveAsync();


            return new()
            {
                Success = true,
                Message = "Book removed succeffuly"
            };

        }

        catch (Exception ex)
        {
            return new()
            {
                Success = false,
                Message = "Book coult not be deleted succeffuly",
                Errors = new List<string> { ex.Message }
            };

        }



        //var  result =   await _bookWriteRepository.RemoveAsync(request.Id.ToString());

        //if (!result)
        //{
        //    return new RemoveProductCommandResponse()
        //    {
        //        Success = false,
        //        Message = "Book not found or could not be deleted"
        //    };
        //}
        //await _bookWriteRepository.SaveAsync();

        //return new RemoveProductCommandResponse()
        //{
        //    Success = true,
        //    Message = "Book removed successfully"
        //};


    }
}