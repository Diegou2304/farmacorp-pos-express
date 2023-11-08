using MediatR;
using Microsoft.AspNetCore.Mvc;
using FarmacorpPOS.Infrastructure.Repositories;

namespace FarmacorpPOS.Application.Features.BarCode.CreateBarCode
{
    public class CreateBarCodeHandler : IRequestHandler<CreateBarCodeCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public CreateBarCodeHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Handle(CreateBarCodeCommand request, CancellationToken cancellationToken)
        {
          
            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);
          
            if (product is null || product.BarCode is not null) return new BadRequestObjectResult(
                        new {Message = "El producto ya tiene asignado un producto de barras ó el código no existe"}
                );

            var newBarCode = Domain.ERP.BarCode.Create(request.Active, product);

            product.AssignBarCode(newBarCode);
            await _unitOfWork.ProductRepository.UpdateProductAsync(product);

            var result = await _unitOfWork.Complete();
            if (result < 0)
            {
                return new BadRequestResult();
            }

            return new OkObjectResult(new { newBarCode.BarCodeId});
        }
    }
}
