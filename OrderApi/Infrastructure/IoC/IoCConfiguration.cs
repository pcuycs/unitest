using Microsoft.Extensions.DependencyInjection;
using Order.BusinessLogic;
using Order.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Order.Api.Infrastructure.IoC
{
    public static class IoCConfiguration
    {
        public static void UseIoC(this IServiceCollection services)
        {
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped<IOrderBusiness, OrderBusiness>();
        }
    }
}
