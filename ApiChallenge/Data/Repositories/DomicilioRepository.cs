using Microsoft.EntityFrameworkCore;
using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public class DomicilioRepository : GenericRepository<Domicilio, int>, IDomicilioRepository
{
    private readonly ApplicationDbContext _context;
    public DomicilioRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
}