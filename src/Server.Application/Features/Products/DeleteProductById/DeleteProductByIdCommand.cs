using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Products.DeleteProductById;

public sealed record DeleteProductByIdCommand(
    string Id) : IRequest<Result<DeleteProductByIdCommandResponse>>;
