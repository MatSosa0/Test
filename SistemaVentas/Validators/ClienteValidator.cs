using FluentValidation;
using SistemaVentas.Models;

namespace SistemaVentas.Validators
{
    public class ClienteValidator : AbstractValidator<Cliente>
    {
        public ClienteValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

            RuleFor(p => p.Email)
                .NotEmpty().WithMessage("El email es obligatorio")
                .EmailAddress().WithMessage("El mail no es válido");
        }
    }
}