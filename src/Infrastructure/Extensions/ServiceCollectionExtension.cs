using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services,
        string? connectionString = null)
    {
        if (connectionString is not null)
            services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
                options.UseSqlite(connectionString));
        else
            services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
                options.UseInMemoryDatabase("ITranslateTrainerInMemoryDb"));

        return services;
    }
}