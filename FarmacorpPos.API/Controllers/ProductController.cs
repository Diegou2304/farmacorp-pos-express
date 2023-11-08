using FarmacorpPOS.Application.Features.Products.AssignCategories;
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

        [HttpPost("/products-categories")]
        public async Task<IActionResult> AssignCategories([FromBody] AssignProductCategoryCommand command )
        {
            return await _mediator.Send(command);
        }
    }
}
