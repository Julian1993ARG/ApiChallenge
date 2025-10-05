using ApiChallenge.Data.Entities.Domicilio;
using ApiChallenge.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Data;

public class ChallengeDbContext : DbContext
{
    public ChallengeDbContext(DbContextOptions<ChallengeDbContext> options) : base(options)
    {
    }

    public virtual DbSet<User> Usuarios { get; set; }

    public virtual DbSet<Domicilio> Domicilio { get; set; }
}
