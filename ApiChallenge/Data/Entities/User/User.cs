using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ApiChallenge.Data.Entities.User;

public class User: BaseEntity<int>
{
    [NotNull, MaxLength(300)]
    public string? Nombre { get; set; }
    [NotNull, MaxLength(300), EmailAddress]
    public string? Email { get; set; }
}
