using MediatR;
using Server.Application.Services;

namespace Server.Application.Behaviours;

public sealed class CacheBehaviour<TRequest, TResponse>(
    ICacheService cacheService) : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (request is ICacheableQuery query)
        {
            var cacheKey = query.CacheKey;
            var cacheTime = query.CacheTime;

            var cachedData = await cacheService
                .GetAsync<TResponse>(cacheKey);

            if (cachedData is not null) return cachedData;

            var response = await next();

            if (response is not null) await cacheService.SetAsync(cacheKey, response, DateTime.Now.AddMinutes(cacheTime));

            return response;
        }

        return await next();
    }
}
