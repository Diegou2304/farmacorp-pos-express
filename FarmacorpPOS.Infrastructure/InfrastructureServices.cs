using FarmacorpPOS.Infrastructure.Repositories;
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
            return services;
        }
    }
}
