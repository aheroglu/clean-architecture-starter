using MapsterMapper;
using MediatR;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.GetAllProducts;

public sealed class GetAllProductsQueryHandler(
    IQueryRepository<Product> queryRepository,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsQueryResponse>>>
{
    private static string key = "getallproducts";
    private static TimeSpan expiration = TimeSpan.FromMinutes(10);

    public async Task<Result<List<GetAllProductsQueryResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var cachedProducts = cacheService
            .Get<List<GetAllProductsQueryResponse>>(key);

        if (cachedProducts is not null) return Result<List<GetAllProductsQueryResponse>>
                .Success(cachedProducts);

        var products = mapper
            .Map<List<GetAllProductsQueryResponse>>(await queryRepository
            .GetAllAsync(cancellationToken));

        cacheService
            .Set(key, products, expiration);

        return new(
            null,
            null,
            products);
    }
}
