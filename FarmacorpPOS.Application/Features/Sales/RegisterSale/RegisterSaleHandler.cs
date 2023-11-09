using FarmacorpPOS.Application.Features.Sales.Utils;
using FarmacorpPOS.Application.Features.Sales.Utils.Factory;
using FarmacorpPOS.Application.Features.Sales.Utils.Strategy;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace FarmacorpPOS.Application.Features.Sales.RegisterSale
{
    public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, IActionResult>
    {
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleStrategyFactory _saleStrategyFactory;
        private readonly string _saleStrategyName;
        private readonly ILogger<RegisterSaleHandler> _logger;
        public RegisterSaleHandler(
                                    IOptions<SaleStrategyConfig> saleStrategyConfig,
                                    IUnitOfWork unitOfWork,
                                    ISaleStrategyFactory saleStrategyFactory,
                                    ILogger<RegisterSaleHandler> logger)
        {
            _saleStrategyName = saleStrategyConfig.Value.SaleMode;
            _unitOfWork = unitOfWork;
            _saleStrategyFactory = saleStrategyFactory;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);
            LogStructure log = new LogStructure
            {
                HttpMethod = HttpMethod.Post,
                Route = "/sales",
                Result = HttpStatusCode.BadRequest,
                Message = $"El producto ingresado no existe en la base de datos",

            };

            if (product is null)
            {
                _logger.LogError("{@log}", log);
                return new BadRequestObjectResult(new { Message = "El producto ingresado no existe en la base de datos" });
            }

            ISaleStrategy saleStrategy = _saleStrategyFactory.GetInstance(_saleStrategyName);

            return await saleStrategy.RegisterSale(
                                        request.ClientFullName,
                                        product, 
                                        request.Quantity);

            
        }

       

    }
}
