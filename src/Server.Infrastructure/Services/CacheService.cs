using Microsoft.Extensions.Caching.Memory;
using Server.Application.Services;

namespace Server.Infrastructure.Services;

public sealed class CacheService(
    IMemoryCache memoryCache) : ICacheService
{
    public void Remove(string key)
    {
        memoryCache.Remove(key);
    }

    public T? Get<T>(string key)
    {
        return memoryCache.TryGetValue(key, out T? value) ? value : default;
    }

    public void Set<T>(string key, T value, TimeSpan expiration)
    {
        memoryCache.Set(key, value, expiration);
    }
}