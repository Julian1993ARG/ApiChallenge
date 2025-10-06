using ApiChallenge.Data.Entities;

namespace ApiChallenge.Services;

public interface IDomicilioService : IGenericService<Domicilio, int>
{
    Task<IEnumerable<Domicilio>> CreateMultipleAsync(IEnumerable<Domicilio> domicilios);
}