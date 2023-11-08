

using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Products.AssignCategories
{
    public class AssignProductCategoriesHandler : IRequestHandler<AssignProductCategoryCommand, IActionResult>
    {
        private readonly IUnitOfWork _unitOfWork;

        public AssignProductCategoriesHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IActionResult> Handle(AssignProductCategoryCommand request, CancellationToken cancellationToken)
        {

            var product = await _unitOfWork.ProductRepository.GetProductById(request.ProductId);
            var category = await _unitOfWork.CategoryRepository.GetCategoryByIdAsync(request.CategoryId);

            if (ProductBelongsToCategory(product, category.CategoryId)) return new BadRequestObjectResult(new { Message = "El producto ya pertenece a la categoria escogida" });
            if (product is not null && category is not null)
            {
                
                product.Categories.Add(category);
                await _unitOfWork.ProductRepository.UpdateProductAsync(product);
                var result = await _unitOfWork.Complete();
                if (result > 0) return new NoContentResult();
                return new BadRequestResult();
            }

            return new BadRequestObjectResult(new { Message = "Por favor, verifique que el producto o la categoria existan" });
           
            
        }

        private bool ProductBelongsToCategory(Product product,int categoryId)
        {
            return product.ProductCategories.Any(c => c.IdCategory == categoryId);

        }
    }
}
