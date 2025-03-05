using BasakSehirBurada.Application.Services.JwtServices;
using BasakSehirBurada.Application.Services.RedisServices;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BasakSehirBurada.Application;

public static class Extensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<IRedisService,RedisCacheService>();

        services.AddScoped<IJwtService, JwtService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
        });
        return services;
    }




    public static bool StartsWithA(this string text)
    {
        return text.StartsWith("A");
    }
}
