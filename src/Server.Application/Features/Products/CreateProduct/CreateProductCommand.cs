using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommand(
    string Name,
    decimal Price,
    int Stock) : IRequest<Result<CreateProductCommandResponse>>;
