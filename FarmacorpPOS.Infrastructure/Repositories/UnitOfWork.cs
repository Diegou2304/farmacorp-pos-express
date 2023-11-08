using FarmacorpPOS.Infrastructure.Persistence;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using System;
using System.Collections;


namespace FarmacorpPOS.Infrastructure.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {        
        private readonly FarmacorpPosDbContext _context;

        private IBarCodeRepository _barCodeRepository;
        private ICategoryRepository _categoryRepository;
        private IProductRepository _productRepository;
        private ISaleRepository _saleRepository;
    
        public IBarCodeRepository BarCodeRepository => _barCodeRepository ??= new BarCodeRepository(_context);

        public ICategoryRepository CategoryRepository => _categoryRepository ??= new CategoryRepository(_context); 

        public IProductRepository ProductRepository => _productRepository ??= new ProductRepository(_context);

        public ISaleRepository SaleRepository => _saleRepository ??= new SaleRepository(_context);

        public UnitOfWork(FarmacorpPosDbContext context)
        {
            _context = context;
        }

        public async Task<int> Complete()
        {
            return await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
        }

       
    }
}
