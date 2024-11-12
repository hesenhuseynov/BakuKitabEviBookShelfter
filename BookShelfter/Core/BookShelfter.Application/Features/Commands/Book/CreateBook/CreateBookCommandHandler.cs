using BookShelfter.Application.Repositories.Book;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.CreateBook
{
    public  class CreateBookCommandHandler:IRequestHandler<CreateBookCommandRequest,CreateBookCommandResponse>
    {
        private readonly IBookWriteRepository _bookWriteRepository;

        public CreateBookCommandHandler(IBookWriteRepository bookWriteRepository)
        {
            _bookWriteRepository = bookWriteRepository;
        }

        public async Task<CreateBookCommandResponse> Handle(CreateBookCommandRequest request, CancellationToken cancellationToken)
        {

            var book = new Domain.Entities.Book
            {
                BookName = request.BookName,
                Price = request.Price,
                Stock = request.Stock,
                AuthorName = request.AuthorName,
                Description = request.Description,
                CategoryId = request.CategoryId,
                LanguageId = request.LanguageId
            };

             
               

          var result  =  await _bookWriteRepository.AddAsync(book);

          if (!result)
              return new()
              {
                  Message = "Book Not Added succeffuly",
                  Success = false
              };

          await _bookWriteRepository.SaveAsync();



          return new CreateBookCommandResponse
          {
              BookId = book.Id,
              BookName = request.BookName,
              Message = "Book added successfully",
              Success = true
          };


        }
    }
}
