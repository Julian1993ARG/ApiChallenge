using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiChallenge.Data.Entities.User;

public class User
{
    public int Id { get; set; }
    [NotNull, MaxLength(300)]
    public string? Nombre { get; set; }
    [NotNull, MaxLength(300), EmailAddress]
    public string? Email { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
