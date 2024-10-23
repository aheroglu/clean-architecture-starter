using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.GetProductById;

public sealed class GetProductByIdQueryHandler(
    IQueryRepository<Product> queryRepository) : IRequestHandler<GetProductByIdQuery, Result<GetProductByIdQueryResponse>>
{
    public async Task<Result<GetProductByIdQueryResponse>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        Product product = await queryRepository
            .GetByAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) return Result<GetProductByIdQueryResponse>.Failure("Product not found!");

        return new(
            null,
            null,
            product.Adapt<GetProductByIdQueryResponse>());
    }
}
