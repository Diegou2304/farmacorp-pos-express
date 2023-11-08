
using Azure.Core;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class NewSaleStrategy : ISaleStrategy
    {
        private readonly ISaleRepository _saleRepository;
        public NewSaleStrategy(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }
        public async Task<IActionResult> RegisterSale(string clientName, Product product, int quantity)
        {
            if (product.ErpProduct!.Stock - quantity <= 10)
                return new BadRequestObjectResult(
                    new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad para que nos queden 10 unidades" });
            var discount = GetDiscount(product);
            var newSale = ExpressSale.CreateSale(clientName, product, quantity, discount);
            product.ErpProduct.DecreaseStock(quantity);
            await _saleRepository.RegisterSale(newSale);
            return new OkResult();

        }
        private static double GetDiscount(Product product) => product.ProductCategories.Count switch
        {
            1 => 0.1,
            > 1 => 0,
            _ => 0

        };
    }
}
