

using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class BaseSaleStrategy : ISaleStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<BaseSaleStrategy> _logger;
        public BaseSaleStrategy(IUnitOfWork unitOfWork, ILogger<BaseSaleStrategy> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> RegisterSale(string clientName, Product product, int quantity)
        {
            LogStructure log = new LogStructure
            {
                HttpMethod = HttpMethod.Post,
                Route = "/sales",
                Result = HttpStatusCode.OK,
                Message = $"La venta del producto: {product.ProductName} fue realizada correctamente",

            };

            if (quantity > product.ErpProduct!.Stock)
            {
                log.Result = HttpStatusCode.BadRequest;
                log.Message = "No hay stock suficiento, por favor vuelva a interntar con otra cantidad";
                _logger.LogError("{@log}", log);
                return new BadRequestObjectResult(new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad" });
            }

            var discount = GetDiscount(product);
            var newSale = ExpressSale.CreateSale(clientName, product, quantity, discount);
            product.ErpProduct.DecreaseStock(quantity);
            await _unitOfWork.SaleRepository.RegisterSale(newSale);
            var result = await _unitOfWork.Complete();
            if (result > 0)
            {
                _logger.LogInformation("{@log}", log);
                return new OkObjectResult(new { newSale.ExpressSaleId });
            }
            
            log.Result = HttpStatusCode.BadRequest;
            log.Message = "Ocurrió un error inesperado al registar la venta";
            _logger.LogError("{@log}", log);

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
