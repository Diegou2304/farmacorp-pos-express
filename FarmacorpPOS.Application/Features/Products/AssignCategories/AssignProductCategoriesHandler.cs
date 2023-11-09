

using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Net;

namespace FarmacorpPOS.Application.Features.Products.AssignCategories
{
    public class AssignProductCategoriesHandler : IRequestHandler<AssignProductCategoryCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ILogger<AssignProductCategoriesHandler> _logger;
        public AssignProductCategoriesHandler(IUnitOfWork unitOfWork, ILogger<AssignProductCategoriesHandler> logger)
        {
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<IActionResult> Handle(AssignProductCategoryCommand request, CancellationToken cancellationToken)
        {

            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.CategoryId);
            LogStructure log = new LogStructure
            {
                HttpMethod = HttpMethod.Post,
                Route = $"/products/{request.ProductId}/categories",
                Result = HttpStatusCode.NoContent,
                Message = "Categoria asignada correctamente",

            };
           
            if (product is not null && category is not null)
            {
                if (ProductBelongsToCategory(product, category.CategoryId))
                {
                    log.Result = HttpStatusCode.BadRequest;
                    log.Message = "El producto ya pertenece a la categoria escogida";
                    _logger.LogError("{@log}", log);
                    return new BadRequestObjectResult(new { Message = "El producto ya pertenece a la categoria escogida" });
                }

                product.Categories.Add(category);
                await _unitOfWork.ProductRepository.UpdateProductAsync(product);
                var result = await _unitOfWork.Complete();
                
                if (result > 0)
                {
                   
                    _logger.LogInformation("{@log}", log);
                    return new NoContentResult();
                }

                log.Result = HttpStatusCode.BadRequest;
                log.Message = "Ocurrió un error en la operación, por favor intente nuevamente";
                _logger.LogError("{@log}", log);
                return new BadRequestResult();
            }

            log.Result = HttpStatusCode.BadRequest;
            log.Message = "Por favor, verifique que el prducto o la categoría existan";
            _logger.LogError("{@log}", log);
            return new BadRequestObjectResult(new { Message = "Por favor, verifique que el producto o la categoria existan" });
           
            
        }

        private bool ProductBelongsToCategory(Product product,int categoryId)
        {
            return product.ProductCategories.Any(c => c.IdCategory == categoryId);

        }
    }
}
