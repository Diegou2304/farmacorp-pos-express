
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmacorpPOS.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            return services;
        }

    }
}
