namespace Server.Infrastructure.Options.Cache;

public sealed class CacheOptions
{
    public string ConnectionString { get; set; } = default!;
    public string InstanceName { get; set; } = default!;
}