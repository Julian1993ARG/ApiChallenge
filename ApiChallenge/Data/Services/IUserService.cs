using ApiChallenge.Data.Entities;

namespace ApiChallenge.Services;

public interface IUserService : IGenericService<User, int>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NombreExistsAsync(string nombre);
    Task<User> CreateUserWithAddressesAsync(User user, IEnumerable<Domicilio> domicilios);
}