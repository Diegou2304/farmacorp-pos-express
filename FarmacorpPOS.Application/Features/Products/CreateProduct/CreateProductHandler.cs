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
        private readonly IProductRepository _productRepository;
        private readonly IOptions<SaleStrategyConfig> _saleStrategyConfig;

        public CreateProductHandler(IProductRepository productRepository, IOptions<SaleStrategyConfig> saleStrategyConfig)
        {
            _productRepository = productRepository;
            _saleStrategyConfig = saleStrategyConfig;
        }
        public async Task<IActionResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var priceMultiplier = 1.5;
            if (_saleStrategyConfig.Value.SaleMode is "NewSale") priceMultiplier = 1.8;
            var productType = await _productRepository.GetProductTypeById(request.ProductTypeId);

            if (productType is null) return new BadRequestObjectResult(new { Message = "El tipo de producto no existe, por favor seleccione otro" });

            var newErpProduct = ErpProduct.CreateErpProduct(request.Cost, request.Stock);
            var newProduct = Product.CreateProduct(
                request.ProductName,
                request.Observations,
                request.ProductTypeId,
                newErpProduct,
                request.ExpirationDate, 
                priceMultiplier);

            await _productRepository.AddProductAsync(newProduct);

            return new CreatedAtRouteResult(newProduct.ProductId, new {});
            
        }
    }
}
