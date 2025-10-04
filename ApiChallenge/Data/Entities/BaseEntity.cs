using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Entities;

public abstract class BaseEntity<TId>
{
    public TId? Id { get; set; }
    public DateTime? FechaCreacion { get; set; }
}

