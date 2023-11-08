

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task UpdateProductAsync(Product product);
    }
}
