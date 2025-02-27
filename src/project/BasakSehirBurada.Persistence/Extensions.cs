using BasakSehirBurada.Application.Services.Repositories;
using BasakSehirBurada.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace BasakSehirBurada.Persistence;

public static class Extensions
{
    public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
    {

        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        return services;
    }
}