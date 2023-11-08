
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Products.AssignCategories
{
    public class AssignProductCategoryCommand : IRequest<IActionResult>
    {
        public DateTime CreationDate { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
