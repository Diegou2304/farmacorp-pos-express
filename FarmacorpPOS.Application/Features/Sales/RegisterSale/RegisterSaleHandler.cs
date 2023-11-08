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
      
        private readonly IProductRepository _productRepository;
        private readonly ISaleStrategyFactory _saleStrategyFactory;
        private readonly string _saleStrategyName;
        public RegisterSaleHandler(IProductRepository productRepository, ISaleStrategyFactory saleStrategyFactory, IOptions<SaleStrategyConfig> saleStrategyConfig)
        {
            _productRepository = productRepository;
            _saleStrategyFactory = saleStrategyFactory;
            _saleStrategyName = saleStrategyConfig.Value.SaleMode;
        }
        public async Task<IActionResult> Handle(RegisterSaleCommand request, CancellationToken cancellationToken)
        {
            

            var product = await _productRepository.GetProductById(request.ProductId);
           

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
