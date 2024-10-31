using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Products.GetProductById;

public sealed record GetProductByIdQuery(
    string Id) : IRequest<Result<GetProductByIdQueryResponse>>;
