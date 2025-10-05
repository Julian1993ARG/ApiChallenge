using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApiChallenge.Data.Entities;

public abstract class BaseEntity<TId>
{
    [Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public TId? Id { get; set; }
    public DateTime? FechaCreacion { get; set; }
}

