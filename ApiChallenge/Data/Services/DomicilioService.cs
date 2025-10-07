using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Services;

public class DomicilioService : GenericService<Domicilio, int>, IDomicilioService
{
    private readonly IDomicilioRepository _domicilioRepository;

    public DomicilioService(IDomicilioRepository domicilioRepository, ChallengeDbContext context)
        : base(domicilioRepository, context)
    {
        _domicilioRepository = domicilioRepository;
    }
    public async Task<IEnumerable<Domicilio>> CreateOrAlterMultipleAsync(IEnumerable<Domicilio> domicilios)
    {
        var entities = new List<Domicilio>();
        
        foreach (var domicilio in domicilios)
        {
            if (domicilio.Id != 0)
            {
                var existingDomicilio = await GetByIdAsync(domicilio.Id);
                if (existingDomicilio != null)
                {
                    _context.Entry(existingDomicilio).State = EntityState.Detached;
                    domicilio.FechaCreacion = existingDomicilio.FechaCreacion;
                    
                    _domicilioRepository.Update(domicilio);
                    entities.Add(domicilio);
                    continue;
                }
            }
            
            domicilio.FechaCreacion = DateTime.UtcNow;
            var created = await _domicilioRepository.Insert(domicilio);
            entities.Add(created);
        }
        
        return entities;
    }
}