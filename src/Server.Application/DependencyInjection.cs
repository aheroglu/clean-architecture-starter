using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using Server.Application.Behaviours;

namespace Server.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services
            .AddMapster();

        TypeAdapterConfig
            .GlobalSettings
            .Scan(assembly);

        services
            .AddMediatR(configuration =>
                configuration
                .RegisterServicesFromAssembly(assembly)
                .AddOpenBehavior(typeof(ValidationBehaviour<,>)));

        services
            .AddValidatorsFromAssembly(assembly);

        return services;
    }
}