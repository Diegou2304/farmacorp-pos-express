using FarmacorpPOS.Domain.Express;
using Microsoft.AspNetCore.Mvc;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public interface ISaleStrategy
    {
        Task<IActionResult> RegisterSale(string clientName, Product product, int quantity);
    }
}
