using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, IActionResult>
    {
        private readonly IProductRepository _productRepository;

        public CreateProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            var productType = await  _productRepository.GetProductTypeById(request.ProductTypeId);

            if (productType is null) return new BadRequestObjectResult(new {Message = "El tipo de producto no existe, por favor seleccione otro"});

            var newErpProduct = ErpProduct.CreateErpProduct(request.Cost, request.Stock);
            var newProduct = Product.CreateProduct(request.ProductName, request.Observations, request.ProductTypeId, newErpProduct, request.ExpirationDate, 1.5);

            await _productRepository.AddProductAsync(newProduct);

            return new CreatedAtRouteResult(newProduct.ProductId, new {});
            
        }
    }
}
