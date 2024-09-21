using Mapster;
using MapsterMapper;
using MediatR;
using Server.Application.Common;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.UpdateProduct;

public sealed class UpdateProductCommandHandler(
    IProductCommandRepository productCommandRepository,
    IProductQueryRepository productQueryRepository,
    IUnitOfWork unitOfWork,
    IMapper mapper) : IRequestHandler<UpdateProductCommand, Result<UpdateProductCommandResponse>>
{
    public async Task<Result<UpdateProductCommandResponse>> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        Product product = await productQueryRepository
            .GetByAsync(p => p.Id == request.Id, cancellationToken);

        if (product is null) return new(null, new List<string> { "Product not found!" }, null);

        if (request.Name != product.Name)
        {
            bool isNameExists = await productQueryRepository
                .IsProductExistsAsync(request.Name, cancellationToken);

            if (isNameExists) return new(null, new List<string> { "Product name already exists!" }, null);
        }

        mapper.Map(request, product);

        productCommandRepository
            .Update(product);

        await unitOfWork
            .SaveChangesAsync(cancellationToken);

        return new(
            "Product was successfully updated",
            null,
            product.Adapt<UpdateProductCommandResponse>());
    }
}
