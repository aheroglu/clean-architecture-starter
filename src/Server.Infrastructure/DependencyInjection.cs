using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Infrastructure.Context;
using Server.Infrastructure.Options.Cache;
using Server.Infrastructure.Options.Jwt;
using Server.Infrastructure.Repositories;
using Server.Infrastructure.Services;

namespace Server.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("SqlServer"));
            });

        services
            .AddIdentityCore<AppUser>()
            .AddRoles<AppRole>()
            .AddEntityFrameworkStores<AppDbContext>();

        services
            .Configure<JwtOptions>(configuration.GetSection("JWT"));

        services
            .ConfigureOptions<JwtSetupOptions>();

        services
            .Configure<CacheOptions>(configuration.GetSection("Redis"));

        services
            .AddTransient<ICacheService, CacheService>();

        services
             .AddStackExchangeRedisCache(options =>
             {
                 options.Configuration = configuration["CacheOptions:ConnectionString"];
                 options.InstanceName = configuration["CacheOptions:InstanceName"];
             });

        services
            .AddAuthentication()
            .AddJwtBearer();

        services
            .AddAuthorization();

        services
            .AddScoped<IUnitOfWork>(options =>
                options.GetRequiredService<AppDbContext>());

        services
            .AddScoped(typeof(IQueryRepository<>), typeof(QueryRepository<>));

        services
            .AddScoped(typeof(ICommandRepository<>), typeof(CommandRepository<>));

        services
            .AddScoped<IProductCommandRepository, ProductCommandRepository>();

        services
            .AddScoped<IProductQueryRepository, ProductQueryRepository>();

        services
            .AddScoped<IJwtProvider, JwtProvider>();

        return services;
    }
}