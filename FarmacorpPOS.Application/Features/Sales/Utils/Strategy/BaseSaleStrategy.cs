

using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class BaseSaleStrategy : ISaleStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        public BaseSaleStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> RegisterSale(string clientName, Product product, int quantity)
        {

            if (quantity > product.ErpProduct!.Stock)
                return new BadRequestObjectResult(new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad" });

            var discount = GetDiscount(product);
            var newSale = ExpressSale.CreateSale(clientName, product, quantity, discount);
            product.ErpProduct.DecreaseStock(quantity);
            await _unitOfWork.SaleRepository.RegisterSale(newSale);
            var result = await _unitOfWork.Complete();
            if(result > 0) return new OkObjectResult(new { newSale.ExpressSaleId });

            return new BadRequestResult();
        }

        private static double GetDiscount(Product product) => product.ProductCategories.Count switch
        {
            1 => 0.3,
            > 1 => 0,
            _ => 0

        };
    }


}
