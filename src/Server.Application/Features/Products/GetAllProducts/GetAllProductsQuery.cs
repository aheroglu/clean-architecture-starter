using MediatR;
using Server.Application.Common;
using Server.Application.Services;

namespace Server.Application.Features.Products.GetAllProducts;

public sealed record GetAllProductsQuery() : IRequest<Result<List<GetAllProductsQueryResponse>>>, ICacheableQuery
{
    public string CacheKey => "GetAllProducts";

    public double CacheTime => 5;
}
