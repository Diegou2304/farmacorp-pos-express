using FarmacorpPOS.Application.Features.Sales.Utils;
using FarmacorpPOS.Domain.ERP;
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Net;

namespace FarmacorpPOS.Application.Features.Products.CreateProduct
{
    public class CreateProductHandler : IRequestHandler<CreateProductCommand, IActionResult>
    {
        private readonly IOptions<SaleStrategyConfig> _saleStrategyConfig;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateProductHandler> _logger;

        public CreateProductHandler(IOptions<SaleStrategyConfig> saleStrategyConfig, IUnitOfWork unitOfWork, ILogger<CreateProductHandler> logger)
        {
            _saleStrategyConfig = saleStrategyConfig;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            LogStructure log = new LogStructure
            {
                HttpMethod = HttpMethod.Post,
                Route = "/products",
                Result = HttpStatusCode.OK,
                Message = $"El producto {request.ProductName} fue creado exitosamente",

            };
            var priceMultiplier = 1.5;
            if (_saleStrategyConfig.Value.SaleMode is "NewSale") priceMultiplier = 1.8;
            var productType = await _unitOfWork.ProductRepository.GetProductTypeById(request.ProductTypeId);


            if (productType is null)
            {
                log.Result = HttpStatusCode.BadRequest;
                log.Message = "El tipo de producto no existe, por favor seleccione otro";
                _logger.LogError("{@log}", log);
                return new BadRequestObjectResult(new { Message = "El tipo de producto no existe, por favor seleccione otro" });
            }
            var newErpProduct = ErpProduct.CreateErpProduct(request.Cost, request.Stock);
            var newProduct = Product.CreateProduct(
                request.ProductName,
                request.Observations,
                request.ProductTypeId,
                newErpProduct,
                request.ExpirationDate,
                priceMultiplier);

            await _unitOfWork.ProductRepository.AddProductAsync(newProduct);
            var result = await _unitOfWork.Complete();
            if (result > 0)
            {
               
                _logger.LogInformation("{@log}", log);
                return new OkObjectResult(new { newProduct.ProductId });
            }
            log.Result = HttpStatusCode.BadRequest;
            log.Message = "Ocurrió un problema al crear el producto";
            _logger.LogError("{@log}", log);
            return new BadRequestResult();


        }
    }
}
