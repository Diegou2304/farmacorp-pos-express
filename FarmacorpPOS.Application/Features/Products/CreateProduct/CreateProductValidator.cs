

using FluentValidation;

namespace FarmacorpPOS.Application.Features.Products.CreateProduct
{
    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator() 
        {
            RuleFor(product => product.ProductName)
            .NotEmpty().WithMessage("El nombre del producto es obligatorio")
            .MaximumLength(100).WithMessage("El nombre del producto no puede superar los 100 caracteres");

            RuleFor(product => product.ExpirationDate)
                .Must(date => date > DateTime.Now).WithMessage("La fecha de vencimiento debe ser en el futuro");

            RuleFor(product => product.Observations)
                .MaximumLength(500).WithMessage("Las observaciones no pueden superar los 500 caracteres");

            RuleFor(product => product.ProductTypeId)
                .GreaterThan(0).WithMessage("El ID del tipo de producto debe ser mayor que cero");

            RuleFor(product => product.Cost)
                .GreaterThan(0).WithMessage("El costo debe ser mayor que cero");

            RuleFor(product => product.Stock)
                .GreaterThanOrEqualTo(0).WithMessage("El stock no puede ser un número negativo");


        }
    }
}
