using BookShelfter.Application.Abstractions.Storage;
using BookShelfter.Application.Repositories.ProductImageFile;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Features.Commands.Book.RemoveBookImage
{
    public class RemoveBookImageCommandHandler : IRequestHandler<RemoveBookImageCommandRequest, RemoveBookImageCommandResponse>
    {
        private readonly IFileStorageService _fileStorageService;
        private readonly IProductImageFileWriteRepository _writeRepository;
        private readonly IProductImageFileReadRepository _readRepository;
        private readonly ILogger<RemoveBookImageCommandHandler> _logger;



        public RemoveBookImageCommandHandler(IFileStorageService fileStorageService, IProductImageFileWriteRepository writeRepository, IProductImageFileReadRepository readRepository, ILogger<RemoveBookImageCommandHandler> logger)
        {
            _fileStorageService = fileStorageService;
            _writeRepository = writeRepository;
            _readRepository = readRepository;
            _logger = logger;
        }

        public async Task<RemoveBookImageCommandResponse> Handle(RemoveBookImageCommandRequest request, CancellationToken cancellationToken)
        {

            if (string.IsNullOrEmpty(request.ImageUrl))
            {
                _logger.LogWarning("Image URL is null or empty");

                return new RemoveBookImageCommandResponse
                {
                    Success = false,
                    Message = "Image Url is null or empty"
                };

            }

            try
            {
                var productImageFile = await _readRepository.GetSingleAsync(p => p.Path == request.ImageUrl);

                if (productImageFile == null)
                {
                    _logger.LogWarning("Image not found: {ImageUrl}", request.ImageUrl);
                    return new RemoveBookImageCommandResponse { Success = false, Message = "Image Not Found" };
                }

                var deleteResult = await _fileStorageService.DeleteFileAsync(request.ImageUrl);

                if (!deleteResult)
                {
                    _logger.LogError("Failed to delete image from cloud storage: {ImageUrl}", request.ImageUrl);
                    return new() { Success = false, Message = "Failed to delete image from cloud stroage" };

                }

                _writeRepository.Remove(productImageFile);
                await _writeRepository.SaveAsync();

                _logger.LogInformation("Image deleted successfully: {ImageUrl}", request.ImageUrl);
                return new() { Success = true, Message = "Image deleted  successfuly" };


            }


            catch (Exception e)
            {
                _logger.LogError(e, "An error occurred while deleting the image: {ImageUrl}", request.ImageUrl);
                return new() { Success = false, Message = $"An error occured while deleting the image  {e.Message}" };
            }

       


        }
    }
}
