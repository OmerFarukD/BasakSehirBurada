using BasakSehirBurada.Application.Services.JwtServices;
using BasakSehirBurada.Application.Services.RedisServices;
using Core.Application.Pipelines.Authorization;
using Core.Application.Pipelines.Caching;
using Core.Application.Pipelines.Loging;
using Core.Application.Pipelines.Performance;
using Core.Application.Pipelines.Validation;
using Core.CrossCuttingConcerns.Logger.Serilog;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace BasakSehirBurada.Application;

public static class Extensions
{

    public static IServiceCollection AddApplicationServices(this IServiceCollection services)
    {

        services.AddScoped<IRedisService,RedisCacheService>();
        services.AddTransient<LoggerServiceBase, MsSqlLogger>();
        services.AddValidatorsFromAssemblies([Assembly.GetExecutingAssembly()]);
        services.AddScoped<IJwtService, JwtService>();

        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(opt =>
        {
            opt.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());

            opt.AddOpenBehavior(typeof(PerformancePipeline<,>));
            opt.AddOpenBehavior(typeof(AuthorizationPipeline<,>));
            opt.AddOpenBehavior(typeof(ValidationPipeline<,>));
            opt.AddOpenBehavior(typeof(LogingPipeline<,>));
            opt.AddOpenBehavior(typeof(CacheRemovePipeline<,>);
        });
        return services;
    }




    public static bool StartsWithA(this string text)
    {
        return text.StartsWith("A");
    }
}
