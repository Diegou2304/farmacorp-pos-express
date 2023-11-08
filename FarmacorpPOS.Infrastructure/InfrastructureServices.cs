using FarmacorpPOS.Infrastructure.Persistence;
using FarmacorpPOS.Infrastructure.Repositories;
using FarmacorpPOS.Infrastructure.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace FarmacorpPOS.Infrastructure
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructureServices(
                                this IServiceCollection services, 
                                IConfiguration configuration)
        {
            services.AddDbContext<FarmacorpPosDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("ConnectionString")
                ));
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IBarCodeRepository, BarCodeRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ISaleRepository, SaleRepository>();
            return services;
        }
    }
}
