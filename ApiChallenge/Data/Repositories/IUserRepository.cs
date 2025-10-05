using ApiChallenge.Data.Entities;

namespace ApiChallenge.Data.Repositories;

public interface IUserRepository : IGenericRepository<User, int>
{
  Task<bool> EmailExistsAsync(string email);
  Task<bool> NombreExistsAsync(string nombre);
}