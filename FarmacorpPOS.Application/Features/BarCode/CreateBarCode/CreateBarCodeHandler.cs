using MediatR;
using Microsoft.AspNetCore.Mvc;
using FarmacorpPOS.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FarmacorpPOS.Application.Features.BarCode.CreateBarCode
{
    public class CreateBarCodeHandler : IRequestHandler<CreateBarCodeCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<CreateBarCodeHandler> _logger;
        public CreateBarCodeHandler(IUnitOfWork unitOfWork, ILogger<CreateBarCodeHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(CreateBarCodeCommand request, CancellationToken cancellationToken)
        {


            LogStructure log = new LogStructure
            {
                HttpMethod = HttpMethod.Post,
                Route = $"/barcodes",
                Result = HttpStatusCode.OK,
                Message = "Asignación de código de barras exitosa",

            };
            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);

            if (product is null || product.BarCode is not null)
            {
                log.Result = HttpStatusCode.BadRequest;
                log.Message = "El producto ya tiene asignado un producto de barras ó el código no existe";
                _logger.LogError("{@log}", log);
                return new BadRequestObjectResult(
                        new { Message = "El producto ya tiene asignado un producto de barras ó el código no existe" });
            }
            var newBarCode = Domain.ERP.BarCode.Create(request.Active, product);

            product.AssignBarCode(newBarCode);
            await _unitOfWork.ProductRepository.UpdateProductAsync(product);
           
            var result = await _unitOfWork.Complete();

         
          
            if (result < 0)
            {
                log.Result = HttpStatusCode.BadRequest;
                log.Message = "El producto ya tiene asignado un producto de barras ó el código no existe";
                _logger.LogError("{@log}", log);
                return new BadRequestResult();
            }

            _logger.LogInformation("{@log}", log);
            return new OkObjectResult(new { newBarCode.BarCodeId});
        }
    }
}
