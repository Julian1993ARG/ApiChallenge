using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Services;

public abstract class GenericService<T, TId> : IGenericService<T, TId>
    where T : BaseEntity<TId>
    where TId : IEquatable<TId>
{
    protected readonly IGenericRepository<T, TId> _repository;
    protected readonly ChallengeDbContext _context;
    private bool _disposed = false;

    protected GenericService(IGenericRepository<T, TId> repository, ChallengeDbContext context)
    {
        _repository = repository;
        _context = context;
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        entity.FechaCreacion = DateTime.UtcNow;
        var createdEntity = await _repository.Insert(entity);
        await SaveChangesAsync();
        return createdEntity;
    }

    public virtual async Task<T?> GetByIdAsync(TId id)
    {
        return await _repository.GetById(id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync()
    {
        return await _repository.GetAll().ToListAsync();
    }

    public virtual async Task<T?> UpdateAsync(TId id, T entity)
    {
        var existingEntity = await _repository.GetById(id);
        if (existingEntity == null)
            return null;

        _context.Entry(existingEntity).State = EntityState.Detached;

        entity.Id = id;
        entity.FechaCreacion = existingEntity.FechaCreacion;        
        _repository.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<bool> DeleteAsync(TId id)
    {
        var result = await _repository.HardDelete(id);
        if (result)
        {
            await SaveChangesAsync();
        }
        return result;
    }

    public virtual async Task<bool> SaveChangesAsync()
    {
        try
        {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
        catch (Exception)
        {
            return false;
        }
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _repository?.Dispose();
                // No disposamos el _context aquí porque es inyectado por DI
                // y debe ser manejado por el contenedor de DI
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