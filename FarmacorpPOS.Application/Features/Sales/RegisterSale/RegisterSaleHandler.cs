using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.SqlExpressions;

namespace FarmacorpPOS.Application.Features.Sales.RegisterSale
{
    public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, IActionResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        public RegisterSaleHandler(ISaleRepository saleRepository, IProductRepository productRepository)
        {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
        }
        public async Task<IActionResult> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _productRepository.GetProductById(request.ProductId);
            var discount = GetDiscount(product);

            if (product is null)
                return new BadRequestObjectResult(new { Message = "El producto ingresado no existe en la base de datos" });
            if (request.Quantity > product.ErpProduct!.Stock)
                return new BadRequestObjectResult(new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad" });

            var newSale = ExpressSale.CreateSale(request.ClientFullName, product, request.Quantity, discount);
            product.ErpProduct.DecreaseStock(request.Quantity);
            await _saleRepository.RegisterSale(newSale);
            return new OkResult();
        }

        private static double GetDiscount(Product product) => product.ProductCategories.Count switch
        {
            1 => 0.3,
            >1 => 0,
            _ => 0

        };
    }
}
