using FarmacorpPOS.Application.Features.Products.AssignCategories;
using FarmacorpPOS.Application.Features.Products.CreateProduct;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPos.API.Controllers
{
    public class ProductController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductController( IMediator mediator)
        {
            _mediator = mediator;
            
        }

        [HttpPost("/products/{productId}/categories")]
        public async Task<IActionResult> AssignCategories(int productId, [FromBody] AssignProductCategoriesRequest request )
        {
            var command = new AssignProductCategoryCommand
            {
                ProductId = productId,
                CategoryId = request.CategoryId,
                CreationDate = request.CreationDate,    
            };
            return await _mediator.Send(command);
        }

        [HttpPost("products")]
        public async Task<IActionResult> CreateProduct([FromBody] CreateProductCommand command)
        {
            return await _mediator.Send(command);   
        }
    }
}
