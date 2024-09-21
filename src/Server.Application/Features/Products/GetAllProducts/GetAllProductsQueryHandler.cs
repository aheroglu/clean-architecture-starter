using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.GetAllProducts;

public sealed class GetAllProductsQueryHandler(
    IQueryRepository<Product> queryRepository) : IRequestHandler<GetAllProductsQuery, Result<List<GetAllProductsQueryResponse>>>
{
    private const string CacheKey = "products";

    public async Task<Result<List<GetAllProductsQueryResponse>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await queryRepository
            .GetAllAsync(cancellationToken);

        return new(
            null,
            null,
            products.Adapt<List<GetAllProductsQueryResponse>>());
    }
}
