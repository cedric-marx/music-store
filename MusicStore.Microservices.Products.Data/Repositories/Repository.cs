using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Microservices.Products.Data.Models.Base;

namespace MusicStore.Microservices.Products.Data.Repositories;

public sealed class Repository<TEntity, TContext> : IRepository<TEntity>
    where TEntity : class, IEntity, new()
    where TContext : DbContext
{
    private readonly TContext _context;
    private readonly DbSet<TEntity> _dbSet;
    private readonly ILogger<Repository<TEntity, TContext>> _logger;

    public Repository(TContext context, ILogger<Repository<TEntity, TContext>> logger)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
        _logger = logger;
    }

    private IQueryable<TEntity> Queryable => _dbSet.AsNoTracking();

    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            _dbSet.Add(entity);

            var isSuccess = await _context.SaveChangesAsync(cancellationToken) > 0;

            _context.Entry(entity).State = EntityState.Detached;

            return isSuccess;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to create entity due to: {Message}", exception.Message);
            throw;
        }
    }

    public async Task<IEnumerable<TEntity>> ReadAllAsync(CancellationToken cancellationToken = default)
    {
        try
        {
            return await Queryable.ToListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to read all entities due to: {Message}", exception.Message);
            throw;
        }
    }

    public async Task<TEntity> ReadAsync(Guid entityId, CancellationToken cancellationToken = default)
    {
        try
        {
            var entity = await Queryable.FirstOrDefaultAsync(x => x.Id == entityId, cancellationToken);
            if (entity == default)
                throw new KeyNotFoundException($"Entity with Id: {entityId} could not be found");

            return entity;
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to read entity with Id: {Id} due to: {Message}", entityId,
                exception.Message);
            throw;
        }
    }

    public async Task<bool> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        _dbSet.Update(entity);

        var success = await _context.SaveChangesAsync(cancellationToken) > 0;

        _context.Entry(entity).State = EntityState.Detached;

        return success;
    }
}