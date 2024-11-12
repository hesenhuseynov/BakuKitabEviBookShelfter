using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookShelfter.Application.Abstractions.Services;
using Microsoft.Extensions.Caching.Memory;

namespace BookShelfter.Infrastructure.Services.Cache
{
    public  class MemoryCacheService:ICacheService
    {
        private readonly IMemoryCache _cache;

        public MemoryCacheService(IMemoryCache cache)
        {
            _cache = cache;
        }

        public T Get<T>(string cacheKey)
        {
            return _cache.TryGetValue(cacheKey, out T value) ? value : default;
        }

        public void Set<T>(string cacheKey, T value, TimeSpan expirationTime)
        {

            _cache.Set(cacheKey, value, expirationTime);

        }

        public bool Remove(string cacheKey)
        {
            _cache.Remove(cacheKey);
            return true;
        }

        public bool TryGetValue<T>(string cacheKey, out T value)
        {
            return _cache.TryGetValue(cacheKey, out value);
        }
    }
}
