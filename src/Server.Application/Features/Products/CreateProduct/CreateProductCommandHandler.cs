using Mapster;
using MediatR;
using Server.Application.Common;
using Server.Application.Services;
using Server.Domain.Entities;
using Server.Domain.Repositories;

namespace Server.Application.Features.Products.CreateProduct;

public sealed class CreateProductCommandHandler(
    IProductCommandRepository productCommandRepository,
    IProductQueryRepository productQueryRepository,
    IUnitOfWork unitOfWork,
    ICacheService cacheService) : IRequestHandler<CreateProductCommand, Result<CreateProductCommandResponse>>
{
    public async Task<Result<CreateProductCommandResponse>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        bool isNameExists = await productQueryRepository
            .IsProductExistsAsync(request.Name, cancellationToken);

        if (isNameExists) return Result<CreateProductCommandResponse>.Failure("Product name already exists!");

        Product product = request.Adapt<Product>();

        await productCommandRepository
            .CreateAsync(product, cancellationToken);

        await unitOfWork
            .SaveChangesAsync(cancellationToken);

        cacheService
            .Remove("getallproducts");

        return Result<CreateProductCommandResponse>.Success(
                "Product was successfully created",
                product.Adapt<CreateProductCommandResponse>()
            );
    }
}