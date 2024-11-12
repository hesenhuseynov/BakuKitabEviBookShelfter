using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Book;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.UpdateBook
{
    public   class UpdateBookCommandHandler:IRequestHandler<UpdateBookCommandRequest,UpdateBookCommandResponse>
    {
        private readonly IBookWriteRepository _writeRepository;
        private readonly IBookReadRepository _bookReadRepository;

        public UpdateBookCommandHandler(IBookWriteRepository writeRepository, IBookReadRepository bookReadRepository)
        {
            _writeRepository = writeRepository;
            _bookReadRepository = bookReadRepository;
        }

        public async  Task<UpdateBookCommandResponse> Handle(UpdateBookCommandRequest request, CancellationToken cancellationToken)
        {
            var existingBook  = await _bookReadRepository.GetByIdAsync(request.BookId);

            if (existingBook is null)
            {
                return new()
                {
                    Success = false,
                    Message = "bele bir kitab tapilmadi",

                };
            }

            existingBook.BookName=request.BookName;
             existingBook.CategoryId=request.CategoryId;
             existingBook.AuthorName = request.AuthorName;
             existingBook.Description=request.Description;
             existingBook.Price = request.Price;
            existingBook.LanguageId= request.LanguageId;
            existingBook.Stock=request.Stock;


          _writeRepository.Update(existingBook);
          



               var result =   await  _writeRepository.SaveAsync();

               if (result > 0)
               {
                   return new UpdateBookCommandResponse
                   {
                       Message = $"{existingBook.Id} ID'li kitap güncellendi",
                       Success = true,
                       BookId = existingBook.Id,
                       BookName = existingBook.BookName
                   };
               }




            return new UpdateBookCommandResponse
            {
                Success = false,
                Message = "Kitap güncellenirken bir xəta baş verdi"
            };







        }
    }
}
