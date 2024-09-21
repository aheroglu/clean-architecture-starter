namespace Server.Application.Features.Products.DeleteProductById;

public sealed record DeleteProductByIdCommandResponse(
    string Id,
    string Name,
    decimal Price,
    int Stock,
    DateTime CreatedAt);
