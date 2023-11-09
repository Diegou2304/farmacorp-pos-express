using FarmacorpPOS.Application.Features.BarCode.CreateBarCode;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPos.API.Controllers
{
    [ApiController]
    public class BarCodeController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BarCodeController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/barcodes")]
        public async Task<IActionResult> CreateBarCode([FromBody] CreateBarCodeCommand command)
        {

            return await _mediator.Send(command);
        }

    }
}
