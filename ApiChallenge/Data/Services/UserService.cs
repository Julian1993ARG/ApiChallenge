using ApiChallenge.Data;
using ApiChallenge.Data.Entities;
using ApiChallenge.Data.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Services;

public class UserService : GenericService<User, int>, IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IDomicilioService _domicilioService;

    public UserService(IUserRepository userRepository, IDomicilioService domicilioService, ChallengeDbContext context)
        : base(userRepository, context)
    {
        _userRepository = userRepository;
        _domicilioService = domicilioService;
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

    public async Task<User> CreateUserWithAddressesAsync(User user, IEnumerable<Domicilio> domicilios)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            if (await EmailExistsAsync(user.Email!))
            {
                throw new InvalidOperationException($"Ya existe un usuario con el email: {user.Email}");
            }

            var createdUser = await CreateAsync(user);

            foreach (var domicilio in domicilios)
            {
                domicilio.UsuarioId = createdUser.Id;
            }

            await _domicilioService.CreateMultipleAsync(domicilios);

            await transaction.CommitAsync();

            var userWithAddresses = await _userRepository.GetAll()
                .Include(u => u.Domicilios)
                .FirstOrDefaultAsync(u => u.Id == createdUser.Id);

            return userWithAddresses ?? createdUser;
        }
        catch
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

    public async Task<List<User>> SearchUsersAsync(string? nombre = null, string? provincia = null, string? ciudad = null)
    {
        var query = _context.Usuarios
              .Include(u => u.Domicilios)
              .AsQueryable();

        if (!string.IsNullOrWhiteSpace(nombre))
            query = query.Where(u => u.Nombre.Contains(nombre));

        if (!string.IsNullOrWhiteSpace(provincia))
            query = query.Where(u => u.Domicilios != null && u.Domicilios.Any(d => d.Provincia != null && d.Provincia.Contains(provincia)));

        if (!string.IsNullOrWhiteSpace(ciudad))
            query = query.Where(u => u.Domicilios != null && u.Domicilios.Any(d => d.Ciudad != null && d.Ciudad.Contains(ciudad)));

        return await query.ToListAsync();
    }
}