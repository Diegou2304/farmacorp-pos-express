using FarmacorpPOS.Application.Features.Sales.Utils.Strategy;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Factory
{
    public class SaleStrategyFactory : ISaleStrategyFactory
    {
        private readonly IEnumerable<ISaleStrategy> _saleStrategies;

        public SaleStrategyFactory(IEnumerable<ISaleStrategy> saleStrategies)
        {
            _saleStrategies = saleStrategies;
        }

        public ISaleStrategy GetInstance(string token)
        {
            return token switch
            {
                "BaseSale" => _saleStrategies.FirstOrDefault(x => x.GetType() == typeof(BaseSaleStrategy)),
                "NewSale" => _saleStrategies.FirstOrDefault(x => x.GetType() == typeof(NewSaleStrategy)),
                _ => _saleStrategies.FirstOrDefault(x => x.GetType() == typeof(BaseSaleStrategy))
            };
        }
    }
}
