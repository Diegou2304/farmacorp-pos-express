

using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Products.AssignCategories
{
    public class AssignProductCategoriesHandler : IRequestHandler<AssignProductCategoryCommand, IActionResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AssignProductCategoriesHandler(IProductRepository productRepository,
            ICategoryRepository categoryRepository)
        {
            _productRepository = productRepository;
            _categoryRepository = categoryRepository;
        }
        public async Task<IActionResult> Handle(AssignProductCategoryCommand request, CancellationToken cancellationToken)
        {

            var product = await _productRepository.GetProductById(request.ProductId);
            var category = await _categoryRepository.GetCategoryByIdAsync(request.CategoryId);

            if (ProductBelongsToCategory(product, category.CategoryId)) return new BadRequestObjectResult(new { Message = "El producto ya pertenece a la categoria escogida" });
            if (product is not null && category is not null)
            {
                
                product.Categories.Add(category);
                await _productRepository.UpdateProductAsync(product);

                return new NoContentResult();
            }

            return new BadRequestObjectResult(new { Message = "Por favor, verifique que el producto o la categoria existan" });
           
            
        }

        private bool ProductBelongsToCategory(Product product,int categoryId)
        {
            return product.ProductCategories.Any(c => c.IdCategory == categoryId);

        }
    }
}
