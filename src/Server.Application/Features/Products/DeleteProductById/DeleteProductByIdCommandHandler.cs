using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.DeleteProductById;

public sealed class DeleteProductByIdCommandHandler(
    ICommandRepository<Product> commandRepository,
    IQueryRepository<Product> queryRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<DeleteProductByIdCommand, Result<DeleteProductByIdCommandResponse>>
{
    public async Task<Result<DeleteProductByIdCommandResponse>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product product = await queryRepository
            .GetByAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) return Result<DeleteProductByIdCommandResponse>.Failure("Product not found!");

        commandRepository
            .Delete(product);

        await unitOfWork
            .SaveChangesAsync(cancellationToken);

        cacheService
            .Remove("getallproducts");

        cacheService
            .Remove("product_" + request.Id);

        return new(
            "Product was successfully deleted",
            null,
            product.Adapt<DeleteProductByIdCommandResponse>());
    }
}
