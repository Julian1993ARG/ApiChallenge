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

    public async Task<User> CreateAlterUserWithAddressesAsync(User user, IEnumerable<Domicilio> domicilios)
    {
        using var transaction = await _context.Database.BeginTransactionAsync();

        try
        {
            User entity;
            if (user.Id == 0)
            {
                // Crear usuario sin SaveChanges
                user.FechaCreacion = DateTime.UtcNow;
                entity = await _userRepository.Insert(user);
            }
            else
            {
                var existingUser = await GetByIdAsync(user.Id);
                if (existingUser == null)
                    throw new KeyNotFoundException($"No se encontró un usuario con Id: {user.Id}");
                    
                _context.Entry(existingUser).State = EntityState.Detached;
                user.FechaCreacion = existingUser.FechaCreacion;
                _userRepository.Update(user);
                entity = user;
            }

            await _context.SaveChangesAsync();

            foreach (var domicilio in domicilios)
                domicilio.UsuarioId = entity.Id;

            await _domicilioService.CreateOrAlterMultipleAsync(domicilios);
            
            await _context.SaveChangesAsync();

            await transaction.CommitAsync();

            // Limpiar el contexto para evitar duplicados por entidades rastreadas
            _context.ChangeTracker.Clear();

            var userWithAddresses = await _userRepository.GetAll()
                .Include(u => u.Domicilios)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.Id == entity.Id);

            return userWithAddresses ?? entity;
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