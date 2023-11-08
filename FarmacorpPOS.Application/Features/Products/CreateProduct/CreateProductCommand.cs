using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Products.CreateProduct
{
    public class CreateProductCommand : IRequest<IActionResult>
    {
        public string ProductName { get; set; }
        public DateTime ExpirationDate {get; set;}
        public string Observations { get; set; }
        public int ProductTypeId { get; set; }
        public double Cost { get; set; }
        public int Stock { get; set; }

    }

   
}
