
using FluentValidation;

namespace FarmacorpPOS.Application.Features.Sales.RegisterSale
{
    public class RegisterSaleValidator : AbstractValidator<RegisterSaleCommand>
    {
        public RegisterSaleValidator() 
        {
            RuleFor(command => command.ClientFullName)
           .NotEmpty().WithMessage("El nombre del cliente es obligatorio");

            RuleFor(command => command.Quantity)
                .GreaterThan(0).WithMessage("La cantidad debe ser mayor que cero");

            RuleFor(command => command.ProductId)
                .GreaterThan(0).WithMessage("El ID del producto debe ser mayor que cero");

        }
    }
}
