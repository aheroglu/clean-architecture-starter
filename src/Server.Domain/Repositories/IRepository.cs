using Server.Domain.Abstractions;
using System.Linq.Expressions;

namespace Server.Domain.Repositories;

public interface ICommandRepository<T> where T : Entity
{
    Task CreateAsync(T entity, CancellationToken cancellationToken = default);
    void Update(T entity);
    void Delete(T entity);
}

public interface IQueryRepository<T> where T : Entity
{
    Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken = default);
    Task<T> GetByAsync(Expression<Func<T, bool>> filter, CancellationToken cancellationToken = default);
}