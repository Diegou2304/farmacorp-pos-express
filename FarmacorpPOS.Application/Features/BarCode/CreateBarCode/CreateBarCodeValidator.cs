

using FluentValidation;
using System.Net;

namespace FarmacorpPOS.Application.Features.BarCode.CreateBarCode
{
    public class CreateBarCodeValidator : AbstractValidator<CreateBarCodeCommand>
    {

        public CreateBarCodeValidator() 
        {
            RuleFor(x => x.ProductId)
                .NotNull()
                .GreaterThanOrEqualTo(1)
                .WithErrorCode(HttpStatusCode.BadRequest.ToString())
                .WithMessage("El campo Product Id debe ser mayor o igual a 1");
            RuleFor(y => y.Active)
                .NotNull();
        }
    }
}
