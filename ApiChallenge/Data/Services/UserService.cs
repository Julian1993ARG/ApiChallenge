using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Services;

public class UserService : GenericService<User, int>, IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository, ChallengeDbContext context)
        : base(userRepository, context)
    {
        _userRepository = userRepository;
    }

    public Task<bool> EmailExistsAsync(string email)
    {
        return _userRepository.EmailExistsAsync(email);
    }

    public Task<bool> NombreExistsAsync(string nombre)
    {
        return _userRepository.NombreExistsAsync(nombre);
    }

  public override async Task<IEnumerable<User>> GetAllAsync()
  {
    var users = await _userRepository.GetAll()
      .Include(u => u.Domicilios) 
      .ToListAsync();
    return users;
  }
}