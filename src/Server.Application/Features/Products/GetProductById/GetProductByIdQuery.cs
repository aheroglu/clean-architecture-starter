using MediatR;
using Server.Application.Common;
using Server.Application.Services;

namespace Server.Application.Features.Products.GetProductById;

public sealed record GetProductByIdQuery(
    string Id) : IRequest<Result<GetProductByIdQueryResponse>>, ICacheableQuery
{
    public string CacheKey => $"GetProductById/{Id}";

    public double CacheTime => 5;
}
