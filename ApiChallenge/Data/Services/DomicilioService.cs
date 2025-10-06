using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;

namespace ApiChallenge.Services;

public class DomicilioService : GenericService<Domicilio, int>, IDomicilioService
{
    private readonly IDomicilioRepository _domicilioRepository;

    public DomicilioService(IDomicilioRepository domicilioRepository, ChallengeDbContext context)
        : base(domicilioRepository, context)
    {
        _domicilioRepository = domicilioRepository;
    }
    public async Task<IEnumerable<Domicilio>> CreateMultipleAsync(IEnumerable<Domicilio> domicilios)
    {
        var createdDomicilios = new List<Domicilio>();
        
        foreach (var domicilio in domicilios)
        {
            var created = await CreateAsync(domicilio);
            createdDomicilios.Add(created);
        }

        return createdDomicilios;
    }
}