using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Application.UnitTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
            options.UseInMemoryDatabase("ITranslateTrainerDb"));
    }
}