using FarmacorpPOS.Application.Features.Sales.RegisterSale;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPos.API.Controllers
{
    [ApiController]
    public class SaleController
    {
        private readonly IMediator _mediator;
        public SaleController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("sales")]
        public  async Task<IActionResult> RegisterSale([FromBody] RegisterSaleCommand command )
        {
            return await _mediator.Send(command);

        }
    }
}
