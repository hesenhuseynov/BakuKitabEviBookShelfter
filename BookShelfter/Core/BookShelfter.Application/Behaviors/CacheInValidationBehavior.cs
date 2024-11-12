using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using BookShelfter.Application.Features.Commands.Book.CreateBook;
using BookShelfter.Application.Features.Commands.Book.RemoveBookImage;
using BookShelfter.Application.Features.Commands.Book.RemoveProduct;
using BookShelfter.Application.Features.Commands.Book.UpdateBook;
using BookShelfter.Application.Features.Commands.Book.UploadBookImage;
using MediatR;
using Serilog;

namespace BookShelfter.Application.Behaviors
{
    public  class CacheInValidationBehavior<TRequest,TResponse>:IPipelineBehavior<TRequest,TResponse>
     where TRequest :IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;
        public readonly ILogger _logger;
        public CacheInValidationBehavior(ICacheService cacheService, ILogger logger)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public  async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            //var cacheKey = $"{typeof(TRequest).Name}-{request.GetHashCode()}";

            _logger.Information("Handling {RequestName}",typeof(TRequest).Name);

            var response = await next();
            if (IsCacheInvalidationRequired(request))
            {
                InvalidateCaches();
            }


            _logger.Information("Handled {RequestName}", typeof(TRequest).Name);
            return response;

        }   

        private bool IsCacheInvalidationRequired(TRequest request)
        {
            return request is CreateBookCommandRequest ||
                   request is UpdateBookCommandRequest ||
                   request is UploadBookImageCommandRequest ||
                   request is RemoveBookImageCommandRequest ||
                   request is RemoveProductCommandRequest;
        }

        private void InvalidateCaches()
        {
            string[] cacheKeys = { "all_books_cache", "new_arrivals_books_cache" };

            foreach (var cacheKey in cacheKeys)
            {
                try
                {
                    _cacheService.Remove(cacheKey);
                    _logger.Information("Cache invalidated for {CacheKey}", cacheKey);
                }
                catch (Exception ex)
                {
                    _logger.Error(ex, "Error invalidating cache for {CacheKey}", cacheKey);
                }
            }
        }
    }
}
