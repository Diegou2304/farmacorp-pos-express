
using Azure.Core;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class BaseSaleStrategy : ISaleStrategy
    {
        private readonly ISaleRepository _saleRepository;
        public BaseSaleStrategy(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<IActionResult> RegisterSale(string clientName, Product product, int quantity)
        {

            if (quantity > product.ErpProduct!.Stock)
                return new BadRequestObjectResult(new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad" });

            var discount = GetDiscount(product);
            var newSale = ExpressSale.CreateSale(clientName, product, quantity, discount);
            product.ErpProduct.DecreaseStock(quantity);
            await _saleRepository.RegisterSale(newSale);
            return new OkResult();
        }

        private static double GetDiscount(Product product) => product.ProductCategories.Count switch
        {
            1 => 0.3,
            > 1 => 0,
            _ => 0

        };
    }


}
