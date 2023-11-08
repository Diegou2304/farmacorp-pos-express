using FarmacorpPOS.Application.Features.Sales.Utils.Strategy;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Factory
{
    public interface ISaleStrategyFactory
    {
        ISaleStrategy GetInstance(string token);
    }
}
