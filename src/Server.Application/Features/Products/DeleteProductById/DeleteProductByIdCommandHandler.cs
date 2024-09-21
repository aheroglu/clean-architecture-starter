using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.DeleteProductById;

public sealed class DeleteProductByIdCommandHandler(
    ICommandRepository<Product> commandRepository,
    IQueryRepository<Product> queryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<DeleteProductByIdCommand, Result<DeleteProductByIdCommandResponse>>
{
    public async Task<Result<DeleteProductByIdCommandResponse>> Handle(DeleteProductByIdCommand request, CancellationToken cancellationToken)
    {
        Product product = await queryRepository
            .GetByAsync(p => p.Id == request.Id, cancellationToken);

        commandRepository
            .Delete(product);

        await unitOfWork
            .SaveChangesAsync(cancellationToken);

        return new(
            "Product was successfully deleted",
            null,
            product.Adapt<DeleteProductByIdCommandResponse>());
    }
}
