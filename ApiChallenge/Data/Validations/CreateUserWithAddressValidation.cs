using ApiChallenge.Data.Entities.Dtos;
using FluentValidation;

namespace ApiChallenge.Data.Validations;

public class CreateUserWithAddressValidation : AbstractValidator<CreateUserWithAddressDto>
{
    public CreateUserWithAddressValidation()
    {
        RuleFor(x => x.Nombre)
            .NotEmpty().WithMessage("El nombre es obligatorio")
            .Length(2, 100).WithMessage("El nombre debe tener entre 2 y 100 caracteres");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es obligatorio")
            .EmailAddress().WithMessage("El formato del email no es válido")
            .MaximumLength(255).WithMessage("El email no puede exceder 255 caracteres");

        RuleFor(x => x.Domicilios)
            .NotEmpty().WithMessage("Debe incluir al menos un domicilio")
            .Must(d => d != null && d.Count > 0).WithMessage("Debe incluir al menos un domicilio");

        RuleForEach(x => x.Domicilios).SetValidator(new CreateDomicilioForUserValidation());
    }
}

public class CreateDomicilioForUserValidation : AbstractValidator<CreateDomicilioForUserDto>
{
    public CreateDomicilioForUserValidation()
    {
        RuleFor(x => x.Calle)
            .NotEmpty().WithMessage("La calle es obligatoria")
            .MaximumLength(200).WithMessage("La calle no puede exceder 200 caracteres");

        RuleFor(x => x.Numero)
            .NotEmpty().WithMessage("El número es obligatorio")
            .MaximumLength(20).WithMessage("El número no puede exceder 20 caracteres");

        RuleFor(x => x.Provincia)
            .NotEmpty().WithMessage("La provincia es obligatoria")
            .MaximumLength(100).WithMessage("La provincia no puede exceder 100 caracteres");

        RuleFor(x => x.Ciudad)
            .NotEmpty().WithMessage("La ciudad es obligatoria")
            .MaximumLength(100).WithMessage("La ciudad no puede exceder 100 caracteres");
    }
}