﻿
using FarmacorpPOS.Application.Features.Sales.Utils;
using FarmacorpPOS.Application.Features.Sales.Utils.Factory;
using FarmacorpPOS.Application.Features.Sales.Utils.Strategy;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace FarmacorpPOS.Application
{
    public static class ApplicationServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
            services.AddScoped<ISaleStrategy, BaseSaleStrategy>();
            services.AddScoped<ISaleStrategy, NewSaleStrategy>();
            services.AddScoped<ISaleStrategyFactory, SaleStrategyFactory>();
            services.Configure<SaleStrategyConfig>(configuration.GetSection("SaleStrategyConfig"));
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));

            return services;
        }

    }
}
