using AutoMapper;
using Microsoft.Extensions.DependencyInjection;

namespace Order.Api.Infrastructure.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static void UseAutoMapper(this IServiceCollection services)
        {
            services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);
        }
    }
}
