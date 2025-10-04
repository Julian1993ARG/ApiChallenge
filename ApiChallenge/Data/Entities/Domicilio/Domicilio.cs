namespace ApiChallenge.Data.Entities.Domicilio;

public class Domicilio
{
    public int Id { get; set; }
    public int UserioId { get; set; }
    public string? Calle { get; set; }
    public string? Numero { get; set; }
    public string? Provinicia { get; set; }
    public string? Ciudad { get; set; }
    public DateTime? FechaCreacion { get; set; }
}
