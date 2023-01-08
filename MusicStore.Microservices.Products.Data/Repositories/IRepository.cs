namespace MusicStore.Microservices.Products.Data.Repositories;

public interface IRepository<TEntity>
{
    Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity> ReadAsync(Guid entityId, CancellationToken cancellationToken = default);

    Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);
}