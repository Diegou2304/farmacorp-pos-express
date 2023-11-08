using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.RegisterSale
{
    public class RegisterSaleCommand : IRequest<IActionResult>
    {
        public string ClientFullName { get; set; }
        public int Quantity { get; set; }
        public int ProductId { get; set; }

    }
}
