using BookShelfter.Application.Abstractions.Storage;
using MediatR;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Repositories.Book;
using BookShelfter.Application.Repositories.ProductImageFile;

namespace BookShelfter.Application.Features.Commands.Book.UploadBookImage
{
    public class UploadBookImageCommandHandler : IRequestHandler<UploadBookImageCommandRequest, UploadBookImageCommandResponse>
    {
        private readonly IFileStorageService _fileStorageService;
        private IBookReadRepository _bookReadRepository;
        private readonly IProductImageFileWriteRepository _writeRepository;


        public UploadBookImageCommandHandler(IFileStorageService fileStorageService, IBookReadRepository bookReadRepository, IProductImageFileWriteRepository writeRepository)
        {

            _fileStorageService = fileStorageService;
            _bookReadRepository = bookReadRepository;
            _writeRepository = writeRepository;
        }



        public async Task<UploadBookImageCommandResponse> Handle(UploadBookImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Book? book = await _bookReadRepository.GetByIdAsync(request.BookId);

            if (book == null)
            {
               return  new UploadBookImageCommandResponse()
                {
                    Success = false,
                    Message = "Book not found"

                };
            }

            using (var stream = request.ImageFile.OpenReadStream())
            {
                var contentType = request.ImageFile.ContentType;
                var imageUrl = await _fileStorageService.UploadFileAsync(stream, request.ImageFile.FileName, contentType);

                


                var productImageFile = new Domain.Entities.ProductImageFile
                {
                    FileName = request.ImageFile.FileName,
                    Path = imageUrl,
                    Storage = "GoogleCloudStorage",
                    ImageUrl = imageUrl,
                    Book = book,
                    BookId = book.Id

                };


                await _writeRepository.AddAsync(productImageFile);
                await _writeRepository.SaveAsync();


                return new UploadBookImageCommandResponse()
                {
                    Success = true,
                    Message = "Image upload successfully",
                    ImageUrl = imageUrl

                };










            }
        }
    }
}
