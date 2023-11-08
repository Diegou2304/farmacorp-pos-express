using FarmacorpPOS.Domain.Express;

namespace FarmacorpPOS.Infrastructure.Repositories.Contracts
{
    public interface ICategoryRepository
    {
        Task<Category> GetCategoryByIdAsync(int id);
    }
}
