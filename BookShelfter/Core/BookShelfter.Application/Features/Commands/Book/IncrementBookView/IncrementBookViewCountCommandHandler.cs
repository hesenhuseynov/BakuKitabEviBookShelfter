using BookShelfter.Application.Repositories.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.IncrementBookView
{
    public  class IncrementBookViewCountCommandHandler:IRequestHandler<IncrementBookViewCountCommandRequest,IncrementBookViewCountCommandResponse>
    {
        private readonly IBookWriteRepository _bookWriteRepository;

        public IncrementBookViewCountCommandHandler(IBookWriteRepository bookWriteRepository)
        {
            _bookWriteRepository = bookWriteRepository;
        }

        public  async Task<IncrementBookViewCountCommandResponse> Handle(IncrementBookViewCountCommandRequest request, CancellationToken cancellationToken)
        {
            var isSuccess = await  _bookWriteRepository.IncrementBookViewCountAsync(request.BookId);

            if (isSuccess)
            {
                return new IncrementBookViewCountCommandResponse
                {
                    IsSuccess = true,
                    Message = "Book view count incremented successfully."
                };
            }

            else
            {
                return new IncrementBookViewCountCommandResponse
                {
                    IsSuccess = false,
                    Message = "Failed to increment book view count."
                };
            }



          
             
        }
    }
}
