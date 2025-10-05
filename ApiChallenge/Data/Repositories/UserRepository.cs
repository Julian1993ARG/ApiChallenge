using Microsoft.EntityFrameworkCore;
using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public class UserRepository : GenericRepository<User, int>, IUserRepository
{
    private readonly ChallengeDbContext _context;
    public UserRepository(ChallengeDbContext context) : base(context)
    {
        _context = context;
    }

  public async Task<bool> EmailExistsAsync(string email)
  {
    return await _context.Usuarios.AnyAsync(u => u.Email == email);
  }

    public Task<bool> NombreExistsAsync(string nombre)
    {
        return _context.Usuarios.AnyAsync(u => u.Nombre == nombre);
    }
}