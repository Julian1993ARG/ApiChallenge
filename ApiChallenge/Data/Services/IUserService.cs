using ApiChallenge.Data.Entities;

namespace ApiChallenge.Services;

public interface IUserService : IGenericService<User, int>
{
    Task<bool> EmailExistsAsync(string email);
    Task<bool> NombreExistsAsync(string nombre);
    Task<User> CreateAlterUserWithAddressesAsync(User user, IEnumerable<Domicilio> domicilios);
    Task<List<User>> SearchUsersAsync(string? nombre = null, string? provincia = null, string? ciudad = null);
}