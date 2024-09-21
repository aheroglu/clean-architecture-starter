namespace Server.Application.Features.Products.GetProductById;

public sealed record GetProductByIdQueryResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime CreatedAt,
    DateTime? UpdatedAt);

