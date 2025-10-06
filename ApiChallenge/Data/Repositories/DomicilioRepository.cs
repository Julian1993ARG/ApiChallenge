using Microsoft.EntityFrameworkCore;
using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public class DomicilioRepository : GenericRepository<Domicilio, int>, IDomicilioRepository
{
    private readonly ChallengeDbContext _context;
    public DomicilioRepository(ChallengeDbContext context) : base(context)
    {
        _context = context;
    }
}