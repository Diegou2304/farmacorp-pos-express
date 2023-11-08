using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Application.Features.Sales.Utils.Strategy
{
    public class SaleContext
    {
        private ISaleStrategy _saleStrategy;

        public void SetSaleStrategy(ISaleStrategy saleStrategy)
        {
            _saleStrategy = saleStrategy;
        }

        public void RegisterSale(string clientName, Product product, int quantity)
        {
            _saleStrategy.RegisterSale(clientName, product, quantity);
        }
    }
}
