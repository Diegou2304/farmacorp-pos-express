

using FarmacorpPOS.Domain.Express;
using Microsoft.EntityFrameworkCore;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly FarmacorpPosDbContext _dbcontext;

        public ProductRepository(FarmacorpPosDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public async Task<Product?> GetProductById(int id)
        {
            return await _dbcontext.products.Include(p => p.BarCode).FirstOrDefaultAsync(d => d.ProductId == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
              _dbcontext.Update(product);

            await _dbcontext.SaveChangesAsync();
        }
    }
}
