namespace Server.Application.Features.Products.GetAllProducts;

public sealed record GetAllProductsQueryResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime CreatedAt,
    DateTime? UpdatedAt);