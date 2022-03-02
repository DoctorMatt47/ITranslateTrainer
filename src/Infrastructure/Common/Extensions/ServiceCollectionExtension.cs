using ITranslateTrainer.Domain.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Infrastructure.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        if (connectionString is not null)
            services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
                options.UseSqlite(connectionString));
        else
            services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
                options.UseInMemoryDatabase("ITranslateTrainerInMemoryDb"));

        return services;
    }
}
