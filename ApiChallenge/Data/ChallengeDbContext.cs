using ApiChallenge.Data.Entities.Domicilio;
using ApiChallenge.Data.Entities.User;
using Microsoft.EntityFrameworkCore;

namespace ApiChallenge.Data;

public class ChallengeDbContext : DbContext
{
    public virtual DbSet<User> Usuarios { get; set; }

    public virtual DbSet<Domicilio> Domicilio { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseMySQL("server=127.0.0.1;port=3306;database=codeChallenge;user=root;password=admin");
}
