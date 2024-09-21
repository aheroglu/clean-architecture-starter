using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandHandler(
    IProductCommandRepository productCommandRepository,
    IProductQueryRepository productQueryRepository,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productQueryRepository
            .IsProductExistsAsync(request.Name, cancellationToken);

        if (isNameExists) return new(null, new List<string> { "Product name already exists!" }, null);

        Product product = request.Adapt<Product>();

        await productCommandRepository
            .CreateAsync(product, cancellationToken);

        await unitOfWork
            .SaveChangesAsync(cancellationToken);

        return new(
            "Product was successfully created",
            null,
            product.Adapt<CreateProductCommandResponse>());
    }
}
