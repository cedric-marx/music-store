namespace MusicStore.Microservices.Orders.Data.Repositories;

public interface IRepository<TEntity>
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default);
}