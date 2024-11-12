using BookShelfter.Application.Repositories.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.MarkBookAsFeatured
{
    public  class MarkBookAsFeaturedCommandHandler:IRequestHandler<MarkBookAsFeaturedCommandRequest,MarkBookAsFeaturedCommandResponse>
    {
        public readonly IBookWriteRepository _bookWriteRepository;

        public MarkBookAsFeaturedCommandHandler(IBookWriteRepository bookWriteRepository)
        {
            _bookWriteRepository = bookWriteRepository;
        }

        public async Task<MarkBookAsFeaturedCommandResponse> Handle(MarkBookAsFeaturedCommandRequest request, CancellationToken cancellationToken)
        {
            var isSuccess = await _bookWriteRepository.MarkBookAsFeaturedAsync(request.BookId);
            if (isSuccess)
            {
                return new()
                {
                    IsSuccess = true,
                    Message = "Book marked as featured successfuly"
                };
            }

            else
            {
                return new()
                {
                    IsSuccess = false,
                    Message = "Failed to mark book as featured."
                };
            }

        }
    }
}
