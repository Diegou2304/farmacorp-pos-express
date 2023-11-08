using FarmacorpPOS.Application.Features.Sales.Utils;
using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FarmacorpPOS.Application.Features.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, IActionResult>
    {
        private readonly IOptions<SaleStrategyConfig> _saleStrategyConfig;
        private readonly IUnitOfWork _unitOfWork;

        public CreateProductHandler(IOptions<SaleStrategyConfig> saleStrategyConfig, IUnitOfWork unitOfWork)
        {
            _saleStrategyConfig = saleStrategyConfig;
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var priceMultiplier = 1.5;
            if (_saleStrategyConfig.Value.SaleMode is "NewSale") priceMultiplier = 1.8;
            var productType = await _unitOfWork.ProductRepository.GetProductTypeById(request.ProductTypeId);

            if (productType is null) return new BadRequestObjectResult(new { Message = "El tipo de producto no existe, por favor seleccione otro" });

            var newErpProduct = ErpProduct.CreateErpProduct(request.Cost, request.Stock);
            var newProduct = Product.CreateProduct(
                request.ProductName,
                request.Observations,
                request.ProductTypeId,
                newErpProduct,
                request.ExpirationDate,
                priceMultiplier);

            await _unitOfWork.ProductRepository.AddProductAsync(newProduct);
            var result = await _unitOfWork.Complete();
            if (result > 0) return new OkObjectResult(new { newProduct.ProductId });

            return new BadRequestResult();


        }
    }
}
