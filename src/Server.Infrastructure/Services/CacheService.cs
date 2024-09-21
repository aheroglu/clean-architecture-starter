using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Server.Application.Services;
using Server.Infrastructure.Options.Cache;
using StackExchange.Redis;

namespace Server.Infrastructure.Services;

public sealed class CacheService : ICacheService
{
    private readonly ConnectionMultiplexer redisConnection;
    private readonly IDatabase database;
    private readonly CacheOptions settings;

    public CacheService(IOptions<CacheOptions> redisOptions)
    {
        settings = redisOptions
            .Value;

        var opt = ConfigurationOptions
            .Parse(settings.ConnectionString);

        redisConnection = ConnectionMultiplexer
            .Connect(opt);

        database = redisConnection
            .GetDatabase();
    }

    public async Task<T> GetAsync<T>(string key)
    {
        var values = await database
            .StringGetAsync(key);

        if (values.HasValue) return JsonConvert.DeserializeObject<T>(values!)!;

        return default!;
    }

    public async Task SetAsync<T>(string key, T value, DateTime? timeout = null)
    {
        TimeSpan expiry = timeout!.Value - DateTime.Now;

        await database
            .StringSetAsync(key, JsonConvert.SerializeObject(value), expiry);
    }
}
