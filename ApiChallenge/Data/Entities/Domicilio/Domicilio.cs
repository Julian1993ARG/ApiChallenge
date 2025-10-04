namespace ApiChallenge.Data.Entities.Domicilio;

public class Domicilio : BaseEntity<int>
{
    public int UserioId { get; set; }
    public string? Calle { get; set; }
    public string? Numero { get; set; }
    public string? Provinicia { get; set; }
    public string? Ciudad { get; set; }
}
