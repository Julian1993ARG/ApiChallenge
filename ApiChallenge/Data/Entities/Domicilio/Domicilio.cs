using System.ComponentModel.DataAnnotations.Schema;

namespace ApiChallenge.Data.Entities;

public class Domicilio : BaseEntity<int>
{
    public int UsuarioId { get; set; }
    public string? Calle { get; set; }
    public string? Numero { get; set; }
    public string? Provinicia { get; set; }
    public string? Ciudad { get; set; }

    [ForeignKey("UsuarioId")]
    public virtual User? Usuario { get; set; }
}
