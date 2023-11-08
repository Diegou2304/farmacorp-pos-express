using FarmacorpPOS.Domain.ERP;

namespace FarmacorpPOS.Infrastructure.Repositories.Contracts
{
    public interface IBarCodeRepository
    {
        Task<BarCode> GetBarCodeByProductIdAsync(int id);
        Task AddBarCodeAsync(BarCode barCode);
    }
}
