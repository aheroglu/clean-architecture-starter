using Server.Domain.Entities;

namespace Server.Domain.Repositories;

public interface IProductQueryRepository : IQueryRepository<Product>
{
    Task<bool> IsProductExistsAsync(string name, CancellationToken cancellationToken = default);
}

public interface IProductCommandRepository : ICommandRepository<Product>
{
}