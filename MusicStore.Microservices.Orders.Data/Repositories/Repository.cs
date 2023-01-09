using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MusicStore.Microservices.Orders.Data.Models.Domain.Base;

namespace MusicStore.Microservices.Orders.Data.Repositories;

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
    
    public async Task<bool> CreateAsync(TEntity entity, CancellationToken cancellationToken = default)
    {
        try
        {
            await _dbSet.AddAsync(entity, cancellationToken);

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
            return await _dbSet.ToListAsync(cancellationToken);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Failed to read all entities due to: {Message}", exception.Message);
            throw;
        }
    }
}