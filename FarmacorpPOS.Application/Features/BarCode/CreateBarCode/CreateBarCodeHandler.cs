using MediatR;
using Microsoft.AspNetCore.Mvc;
using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;

namespace FarmacorpPOS.Application.Features.BarCode.CreateBarCode
{
    public class CreateBarCodeHandler : IRequestHandler<CreateBarCodeCommand, IActionResult>
    {
        private readonly IProductRepository _productRepository;

        public CreateBarCodeHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Handle(CreateBarCodeCommand request, CancellationToken cancellationToken)
        {
          

            var product = await _productRepository.GetProductById(request.ProductId);
          
            if (product is null || product.BarCode is not null) return new BadRequestObjectResult(
                        new {Message = "El producto ya tiene asignado un producto de barras ó el código no existe"}
                );

            var newBarCode = Domain.ERP.BarCode.Create(request.Active, product);

            product.BarCode = newBarCode;

            await _productRepository.UpdateProductAsync(product);

            return new OkObjectResult(new { newBarCode.BarCodeId});
        }
    }
}
