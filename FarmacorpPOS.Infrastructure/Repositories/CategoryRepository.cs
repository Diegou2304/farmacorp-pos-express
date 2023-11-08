

using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly FarmacorpPosDbContext _dbContext;
        public CategoryRepository(FarmacorpPosDbContext dbContext)
        {

            _dbContext = dbContext;

        }
        public async Task<Category?> GetCategoryByIdAsync(int id)
        {
           return await _dbContext.Categories.FindAsync(id);  
        }
    }
}
