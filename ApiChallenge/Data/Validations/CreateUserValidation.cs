

using ApiChallenge.Data.Entities;
using ApiChallenge.Services;
using FluentValidation;

namespace ApiChallenge.Data.Validations;

public class CreateUserValidation : AbstractValidator<User>
{
    private readonly IUserService _userService;

    public CreateUserValidation(IUserService userService)
    {
        _userService = userService;

        RuleFor(x => x.Nombre).NotEmpty().WithMessage("El nombre es obligatorio")
                .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres")
                .Matches(@"^[a-zA-ZáéíóúÁÉÍÓÚñÑ\s]+$").WithMessage("El nombre solo puede contener letras y espacios")
                .MustAsync(BeUniqueNombre).WithMessage("Ya existe un usuario con este nombre");

        RuleFor(x => x.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El formato del email no es válido")
                .MaximumLength(255).WithMessage("El email no puede exceder 255 caracteres")
                .MustAsync(BeUniqueEmail).WithMessage("Ya existe un usuario con este email");

    }

    private async Task<bool> BeUniqueEmail(string email, CancellationToken token)
    {
        return !await _userService.EmailExistsAsync(email);
    }

    private async Task<bool> BeUniqueNombre(string nombre, CancellationToken token)
    {
        return !await _userService.NombreExistsAsync(nombre);
    }
}