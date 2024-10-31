using MediatR;
using Server.Application.Common;

namespace Server.Application.Features.Products.GetAllProducts;

public sealed record GetAllProductsQuery() : IRequest<Result<List<GetAllProductsQueryResponse>>>;
