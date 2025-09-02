using Mapster;
using MapsterMapper;
using Microsoft.Extensions.DependencyInjection;

namespace EduCoreApi.Application;

public static class ConfigureServices
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        var config = TypeAdapterConfig.GlobalSettings;
        config.Scan(typeof(ConfigureServices).Assembly); // IRegister имплементацияларни олади
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));
        services.AddSingleton(config);                   // DI'га қўшиш
        services.AddScoped<IMapper, ServiceMapper>();    // IMapper service

        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(ConfigureServices).Assembly));

        services.AddSingleton<TimeProvider>(TimeProvider.System);


        return services;
    }
}
