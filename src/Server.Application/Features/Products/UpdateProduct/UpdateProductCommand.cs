using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommand(
    string Id,
    string Name,
    decimal Price,
    int Stock) : IRequest<Result<UpdateProductCommandResponse>>;
