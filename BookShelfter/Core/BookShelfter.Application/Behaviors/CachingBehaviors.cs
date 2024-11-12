using BookShelfter.Application.Abstractions.Services;
using MediatR;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Features.Queries.Book.GetAllBook;
using BookShelfter.Application.Features.Queries.Book.GetNewArrivalsBooks;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;

namespace BookShelfter.Application.Behaviors
{
    public class CachingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, 
        TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<CachingBehavior<TRequest, TResponse>> _logger;
        private readonly TimeSpan _expirationTime = TimeSpan.FromSeconds(34);

        public CachingBehavior(ICacheService cacheService, ILogger<CachingBehavior<TRequest, TResponse>> logger)
        {
            _cacheService = cacheService;
           _logger = logger;
        } 

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (ShouldCache(typeof(TRequest)))
            {
                //var cacheKey = $"{typeof(TRequest).Name}-{request.GetHashCode()}";
                string  cacheKey = GenerateCacheKey(request);

                //var cacheKey = $"{typeof(TRequest).Name}-{JsonConvert.SerializeObject(request)}";

                _logger.LogInformation("Checking cache for {Cachekey}", cacheKey);

                try
                {
                    var cachedResponse = _cacheService.Get<TResponse>(cacheKey);

                    if (cachedResponse != null)
                    {
                        _logger.LogInformation("Cache hit for {CacheKey}", cacheKey);
                        return cachedResponse;
                    }
                }

                catch (Exception ex )
                {
                    _logger.LogError(ex, "Error retrieving cache for {CacheKey}", cacheKey);



                }



                var response = await next();
                _cacheService.Set(cacheKey, response, _expirationTime);

                return response;
            }
            else
            {
                return await next();
            }
        }

        private bool ShouldCache(Type requestType)
        {
            return requestType == typeof(GetAllBookQueryRequest) ||
                   requestType==typeof(GetNewArrivalsBooksQueryRequest);
        }



        private string GenerateCacheKey(TRequest request)
        {
            return $"{typeof(TRequest).Name}-{JsonConvert.SerializeObject(request)}";
        }
    }

}

