namespace Server.Application.Features.Products.CreateProduct;

public sealed record CreateProductCommandResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime CreatedAt);
