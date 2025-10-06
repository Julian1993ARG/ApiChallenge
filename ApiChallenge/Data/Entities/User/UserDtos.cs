

using System.ComponentModel.DataAnnotations;

namespace ApiChallenge.Data.Entities.Dtos;

public class CreateUserDto
{
  [Required(ErrorMessage = "El nombre es obligatorio")]
  [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
  public string Nombre { get; set; } = string.Empty;

  [Required(ErrorMessage = "El email es obligatorio")]
  [EmailAddress(ErrorMessage = "El formato del email no es válido")]
  [StringLength(255, ErrorMessage = "El email no puede exceder 255 caracteres")]
  public string Email { get; set; } = string.Empty;
}

public class CreateUserWithAddressDto : CreateUserDto
{
  [Required(ErrorMessage = "Debe incluir al menos un domicilio")]
  [MinLength(1, ErrorMessage = "Debe incluir al menos un domicilio")]
  public List<CreateDomicilioForUserDto>? Domicilios { get; set; }
}
public class UpdateUserDto
{
  [StringLength(100, MinimumLength = 2, ErrorMessage = "El nombre debe tener entre 2 y 100 caracteres")]
  public string? Nombre { get; set; }

  [EmailAddress(ErrorMessage = "El formato del email no es válido")]
  [StringLength(255, ErrorMessage = "El email no puede exceder 255 caracteres")]
  public string? Email { get; set; }
}

public readonly record struct UserResponseDto(int Id, string Nombre, string Email);
public readonly record struct UserWithAddressResponseDto(int Id, string Nombre, string Email, IEnumerable<DomicilioResponseDto> Domicilios);
