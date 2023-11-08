using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories.Contracts
{
    public interface ISaleRepository
    {
        Task RegisterSale(ExpressSale expressSale);
    }
}
