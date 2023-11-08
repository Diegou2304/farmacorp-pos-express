using FarmacorpPOS.Infrastructure.Repositories.Contracts;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public interface IUnitOfWork : IDisposable
    {
        IBarCodeRepository BarCodeRepository { get; }
        ICategoryRepository CategoryRepository { get; }
        IProductRepository ProductRepository { get; }
        ISaleRepository SaleRepository { get; }
        Task<int> Complete();
    }
}