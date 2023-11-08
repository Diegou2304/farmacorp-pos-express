

using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace FarmacorpPOS.Application.Features.BarCode.CreateBarCode
{
    public class CreateBarCodeCommand : IRequest<IActionResult>
    {
        public int ProductId { get; set; }
        public bool Active { get; set; }
    }
}
