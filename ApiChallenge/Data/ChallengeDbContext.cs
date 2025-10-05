using ApiChallenge.Data.Entities;
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
