namespace Server.Application.Services;

public interface ICacheService
{
    Task<T> GetAsync<T>(string key);
    Task SetAsync<T>(string key, T value, DateTime? timeout = null);
}
