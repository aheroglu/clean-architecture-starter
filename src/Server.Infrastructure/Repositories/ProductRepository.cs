using Microsoft.EntityFrameworkCore;
using Server.Domain.Entities;
using Server.Domain.Repositories;
using Server.Infrastructure.Context;

namespace Server.Infrastructure.Repositories;

public sealed class ProductQueryRepository : QueryRepository<Product>, IProductQueryRepository
{
    public ProductQueryRepository(AppDbContext context) : base(context)
    {
    }

    public async Task<bool> IsProductExistsAsync(string name, CancellationToken cancellationToken = default)
    {
        return await context.Set<Product>().AnyAsync(p => p.Name == name, cancellationToken);
    }
}

public sealed class ProductCommandRepository : CommandRepository<Product>, IProductCommandRepository
{
    public ProductCommandRepository(AppDbContext context) : base(context)
    {
    }
}