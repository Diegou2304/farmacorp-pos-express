
using FarmacorpPOS.Domain.ERP;
using Microsoft.EntityFrameworkCore;

namespace FarmacorpPOS.Infrastructure.Repositories
{
    public class BarCodeRepository : IBarCodeRepository
    {
        private readonly FarmacorpPosDbContext _dbContext;

        public BarCodeRepository(FarmacorpPosDbContext dbContext)
        {
            _dbContext = dbContext;
        }

      
        public async Task AddBarCodeAsync(BarCode barCode)
        {
            await _dbContext.AddAsync(barCode);
        }

        public async Task<BarCode?> GetBarCodeByProductIdAsync(int id)
        {
            return await _dbContext.BarCodes
               .FirstOrDefaultAsync(d => d.Product!.ProductId == id);
        }
    }
}
