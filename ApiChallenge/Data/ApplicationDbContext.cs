using ApiChallenge.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Usuarios { get; set; }

    public virtual DbSet<Domicilio> Domicilios { get; set; }
}
