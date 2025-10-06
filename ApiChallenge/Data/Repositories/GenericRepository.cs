using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public abstract class GenericRepository<T, TId> : IGenericRepository<T, TId>
    where T : BaseEntity<TId>
    where TId : IEquatable<TId>
{
    private readonly ChallengeDbContext _context;
    private bool _disposed = false;
    protected DbSet<T> Entities => _context.Set<T>();

    protected GenericRepository(ChallengeDbContext context)
    {
        _context = context;
    }

    public async Task<T> Insert(T entity)
    {
        EntityEntry<T> insertedValue = await _context.Set<T>().AddAsync(entity);
        return insertedValue.Entity;
    }

    public virtual async Task<T?> GetById(TId id)
    {
        if (id == null)
            return null;
            
        return await _context.Set<T>()
            .FirstOrDefaultAsync(a => EqualityComparer<TId>.Default.Equals(a.Id, id));
    }


    public IQueryable<T> GetAll()
        => _context.Set<T>();

    public void Update(T entity)
    {
        _context.Set<T>().Update(entity);
    }

    public async Task<bool> HardDelete(TId id)
    {
        T? entity = await GetById(id);
        if (entity is null)
            return false;
        _context.Set<T>().Remove(entity);
        return true;
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context?.Dispose();
            }
            _disposed = true;
        }
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}