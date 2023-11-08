using FarmacorpPOS.Application.Features.BarCode.CreateBarCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPos.API.Controllers
{
    [ApiController]
    public class BarCode : ControllerBase
    {
        private readonly IMediator _mediator;

        public BarCode(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/barcode")]
        public async Task<IActionResult> CreateBarCode([FromBody] CreateBarCodeCommand command)
        {

            return await _mediator.Send(command);
        }

    }
}
