

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public interface ISaleRepository
    {
        Task RegisterSale(ExpressSale expressSale);
    }
}
