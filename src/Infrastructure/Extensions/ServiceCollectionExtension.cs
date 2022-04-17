using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence.Contexts;
using ITranslateTrainer.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Infrastructure.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options => options.UseSqlite(connectionString));
        services.AddTransient<ITranslationSheetService, TranslationSheetService>();
        return services;
    }
}