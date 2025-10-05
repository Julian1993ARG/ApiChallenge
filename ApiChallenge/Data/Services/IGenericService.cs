using ApiChallenge.Data.Entities;

namespace ApiChallenge.Services;

public interface IGenericService<T, TId>
    where T : BaseEntity<TId>
{
    Task<T> CreateAsync(T entity);
    Task<T?> GetByIdAsync(TId id);
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> UpdateAsync(TId id, T entity);
    Task<bool> DeleteAsync(TId id);
    Task<bool> SaveChangesAsync();
}