

using FluentValidation;

namespace FarmacorpPOS.Application.Features.Products.AssignCategories
{
    public class AssignProductCategoriesValidator : AbstractValidator<AssignProductCategoriesRequest>
    {
        public AssignProductCategoriesValidator() 
        {
            RuleFor(product => product.CategoryId)
            .GreaterThanOrEqualTo(1)
            .WithMessage("El ID de la categoría debe ser mayor que cero");

        }
    }
}
