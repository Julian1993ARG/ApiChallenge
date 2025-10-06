using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;
public interface IGenericRepository<T, TId> : IDisposable
    where T : BaseEntity<TId>
{
    Task<T> Insert(T entity);
    Task<T?> GetById(TId id);
    IQueryable<T> GetAll();
    void Update(T entity);
    Task<bool> HardDelete(TId id);
}