using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookShelfter.Application.Abstractions.Services
{
    public interface ICacheService
    {
        T Get<T>(string cacheKey);

        void Set<T>(string cacheKey, T value, TimeSpan expirationTime);
        bool Remove(string cacheKey);
        bool TryGetValue<T>(string cacheKey, out T value);
    }
}
