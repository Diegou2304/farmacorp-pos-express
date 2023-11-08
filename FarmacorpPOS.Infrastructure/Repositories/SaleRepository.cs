
using FarmacorpPOS.Domain.Express;
using FarmacorpPOS.Infrastructure.Persistence;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public class SaleRepository : ISaleRepository 
    {
        private readonly FarmacorpPosDbContext _dbContext;
        public SaleRepository(FarmacorpPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task RegisterSale(ExpressSale expressSale)
        {
            await _dbContext.ExpressSale.AddAsync(expressSale);
            await _dbContext.SaveChangesAsync();
        }

      
    }
}
