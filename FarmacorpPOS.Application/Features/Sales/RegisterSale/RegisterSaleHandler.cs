using FarmacorpPOS.Application.Features.Sales.Utils;
using FarmacorpPOS.Application.Features.Sales.Utils.Factory;
using FarmacorpPOS.Application.Features.Sales.Utils.Strategy;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace FarmacorpPOS.Application.Features.Sales.RegisterSale
{
    public class RegisterSaleHandler : IRequestHandler<RegisterSaleCommand, IActionResult>
    {
      
        private readonly IUnitOfWork _unitOfWork;
        private readonly ISaleStrategyFactory _saleStrategyFactory;
        private readonly string _saleStrategyName;
        public RegisterSaleHandler(
                                    IOptions<SaleStrategyConfig> saleStrategyConfig,
                                    IUnitOfWork unitOfWork,
                                    ISaleStrategyFactory saleStrategyFactory)
        {
            _saleStrategyName = saleStrategyConfig.Value.SaleMode;
            _unitOfWork = unitOfWork;
            _saleStrategyFactory = saleStrategyFactory;
        }
        public async Task<IActionResult> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);
           

            if (product is null)
                return new BadRequestObjectResult(new { Message = "El producto ingresado no existe en la base de datos" });

            ISaleStrategy saleStrategy = _saleStrategyFactory.GetInstance(_saleStrategyName);

            return await saleStrategy.RegisterSale(
                                        request.ClientFullName,
                                        product, 
                                        request.Quantity);

            
        }

       

    }
}
