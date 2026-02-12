using FluentValidation;
using SistemaVentas.Models;

namespace SistemaVentas.Validators
{
    public class ProductoValidator : AbstractValidator<Producto>
    {
        public ProductoValidator()
        {
            RuleFor(p => p.Nombre)
                .NotEmpty().WithMessage("El nombre es obligatorio")
                .MaximumLength(100).WithMessage("El nombre no puede superar los 100 caracteres");

            RuleFor(p => p.Precio)
                .GreaterThan(0).WithMessage("El precio debe ser mayor a cero");

            RuleFor(p => p.stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser negativo");
        }
    }
}