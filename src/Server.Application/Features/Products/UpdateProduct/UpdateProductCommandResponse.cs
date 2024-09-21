namespace Server.Application.Features.Products.UpdateProduct;

public sealed record UpdateProductCommandResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime CreatedAt,
    DateTime? UpdatedAt);