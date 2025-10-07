using ApiChallenge.Data.Entities;

namespace ApiChallenge.Services;

public interface IDomicilioService : IGenericService<Domicilio, int>
{
    Task<IEnumerable<Domicilio>> CreateOrAlterMultipleAsync(IEnumerable<Domicilio> domicilios);
}