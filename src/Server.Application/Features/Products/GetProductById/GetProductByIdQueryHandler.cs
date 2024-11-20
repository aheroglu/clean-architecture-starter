using MapsterMapper;
using MediatR;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.GetProductById;

public sealed class GetProductByIdQueryHandler(
    IQueryRepository<Product> queryRepository,
    IMapper mapper,
    ICacheService cacheService) : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdQueryResponse>>
{
    private static TimeSpan expiration = TimeSpan.FromMinutes(10);

    public async Task<Result<GetProductByIdQueryResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        string key = "product_" + request.Id;

        var cachedProduct = cacheService
            .Get<GetProductByIdQueryResponse>(key);

        if (cachedProduct is not null) return Result<GetProductByIdQueryResponse>
                .Success(cachedProduct);

        var product = mapper
            .Map<GetProductByIdQueryResponse>(await queryRepository
            .GetByAsync(p => p.Id == request.Id, cancellationToken));

        if (product is null) return Result<GetProductByIdQueryResponse>
                .Failure("Product not found!");

        cacheService
            .Set(key, product, expiration);

        return Result<GetProductByIdQueryResponse>
            .Success(product);
    }
}
