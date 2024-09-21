namespace Server.Application.Services;

public interface ICacheableQuery
{
    string CacheKey { get; }
    double CacheTime { get; }
}
