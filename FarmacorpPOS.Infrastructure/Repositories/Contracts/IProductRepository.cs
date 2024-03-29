﻿using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories.Contracts
{
    public interface IProductRepository
    {
        Task<Product> GetProductById(int id);
        Task UpdateProductAsync(Product product);
        Task<ProductType?> GetProductTypeById(int id);
        Task AddProductAsync(Product product);
    }
}
