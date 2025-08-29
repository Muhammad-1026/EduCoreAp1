using FluentValidation;
using Mapster;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace EduCoreApi.Application
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {

            var config = TypeAdapterConfig.GlobalSettings;
            config.Scan(typeof(ConfigureServices).Assembly); // IRegister имплементацияларни олади
            services.AddSingleton(config);                   // DI'га қўшиш
            services.AddScoped<IMapper, ServiceMapper>();    // IMapper service

            services.AddMediatR(cfg =>
                cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));


            return services;
        }
    }
}
