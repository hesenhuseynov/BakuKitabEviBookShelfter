using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Storage;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Application.Repositories.ProductImageFile;
using BookShelfter.Domain.Entities;
using MediatR;

namespace BookShelfter.Application.Features.Commands.Book.CreateBookWithImage
{
    public class CreateBookWithImageCommandHandler : IRequestHandler<CreateBookWithImageCommandRequest, CreateBookWithImageCommandResponse>
    {
        private readonly IBookWriteRepository _bookWriteRepository;
        private readonly IFileStorageService _fileStorageService;
        private readonly IProductImageFileWriteRepository _productImageFileWriteRepository;

        public CreateBookWithImageCommandHandler(IBookWriteRepository bookWriteRepository, IFileStorageService fileStorageService, IProductImageFileWriteRepository productImageFileWriteRepository)
        {
            _bookWriteRepository = bookWriteRepository;
            _fileStorageService = fileStorageService;
            _productImageFileWriteRepository = productImageFileWriteRepository;
        }

        public async  Task<CreateBookWithImageCommandResponse> Handle(CreateBookWithImageCommandRequest request, CancellationToken cancellationToken)
        {
            string imageUrl = null;


            if (request.ImageFile != null)
            {
                using (var stream = request.ImageFile.OpenReadStream())
                {
                    var contentType = request.ImageFile.ContentType;
                    imageUrl = await _fileStorageService.UploadFileAsync(stream, request.ImageFile.FileName, contentType);
                }
            }


            var book = new Domain.Entities.Book
            {
                BookName = request.BookName,
                Price = request.Price,
                Stock = request.Stock,
                AuthorName = request.AuthorName,
                Description = request.Description,
                CategoryId = request.CategoryId
            };

            await _bookWriteRepository.AddAsync(book);
            await _bookWriteRepository.SaveAsync();


            if (imageUrl!=null)
            {
                var productImageFIle = new Domain.Entities.ProductImageFile
                {
                    ImageUrl = imageUrl,
                    FileName = Path.GetFileName(imageUrl),
                    Path = imageUrl,
                    Storage = "GoogleCloudStorage",
                    BookId = book.Id,
                    Book = book
                };

                await _productImageFileWriteRepository.AddAsync(productImageFIle);
                await _productImageFileWriteRepository.SaveAsync();

            }

            return new CreateBookWithImageCommandResponse
            {
                BookId = book.Id,
                ImageUrl = imageUrl
            };


        }
    }
}
