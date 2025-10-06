using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Entities.Dtos;

public class CreateDomicilioDto
{
    [Required(ErrorMessage = "La calle es obligatoria")]
    [StringLength(200, ErrorMessage = "La calle no puede exceder 200 caracteres")]
    public string Calle { get; set; } = string.Empty;

    [Required(ErrorMessage = "El número es obligatorio")]
    [StringLength(20, ErrorMessage = "El número no puede exceder 20 caracteres")]
    public string Numero { get; set; } = string.Empty;

    [Required(ErrorMessage = "La provincia es obligatoria")]
    [StringLength(100, ErrorMessage = "La provincia no puede exceder 100 caracteres")]
    public string Provinicia { get; set; } = string.Empty;

    [Required(ErrorMessage = "La ciudad es obligatoria")]
    [StringLength(100, ErrorMessage = "La ciudad no puede exceder 100 caracteres")]
    public string Ciudad { get; set; } = string.Empty;
}

public class UpdateDomicilioDto
{
    public string? Calle { get; set; }
    public string? Numero { get; set; }
    public string? Provinicia { get; set; }
    public string? Ciudad { get; set; }
}

public readonly record struct DomicilioResponseDto(int Id, string Calle, string Numero, string Provinicia, string Ciudad);