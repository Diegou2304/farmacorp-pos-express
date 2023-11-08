using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class NewSaleStrategy : ISaleStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        public NewSaleStrategy(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> RegisterSale(string clientName, Product product, int quantity)
        {
            if (product.ErpProduct!.Stock - quantity <= 10)
                return new BadRequestObjectResult(
                    new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad para que nos queden 10 unidades" });
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
            1 => 0.1,
            > 1 => 0,
            _ => 0

        };
    }
}
