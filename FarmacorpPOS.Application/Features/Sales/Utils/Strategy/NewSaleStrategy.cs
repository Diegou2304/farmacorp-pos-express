using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Serilog;
using System.Net;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class NewSaleStrategy : ISaleStrategy
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<NewSaleStrategy> _logger;

       
        public NewSaleStrategy(IUnitOfWork unitOfWork, ILogger<NewSaleStrategy> logger)
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

            if (product.ErpProduct!.Stock - quantity <= 10)
            {
                log.Result = HttpStatusCode.BadRequest;
                log.Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad para que nos queden 10 unidades";
                _logger.LogError("{@log}", log);


                return new BadRequestObjectResult(
                    new { Message = "No hay stock suficiente, por favor vuelva a intentar con otra cantidad para que nos queden 10 unidades" });

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
            log.Message = "Ocurrio un error inesperado al registar la venta";
                _logger.LogError("{@log}", log);
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
