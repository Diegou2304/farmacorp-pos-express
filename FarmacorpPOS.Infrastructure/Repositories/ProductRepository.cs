

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
            return await _dbcontext.products.AsSplitQuery()
                            .Include(s => s.ProductCategories)
                            .Include(p => p.BarCode)
                            .FirstOrDefaultAsync(d => d.ProductId == id);
        }

        public async Task UpdateProductAsync(Product product)
        {
              _dbcontext.Update(product);

            await _dbcontext.SaveChangesAsync();
        }

        public async Task<ProductType?> GetProductTypeById(int id)
        {
            return await _dbcontext.productsType!.FindAsync(id);
        }
        public async Task AddProductAsync(Product product)
        {
            await _dbcontext.products.AddAsync(product);
            await _dbcontext.SaveChangesAsync();
        }

    }
}
