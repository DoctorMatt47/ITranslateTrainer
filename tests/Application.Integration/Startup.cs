using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Application.IntegrationTests.Common.Mocks;
using ITranslateTrainer.Infrastructure.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Application.IntegrationTests;

public class Startup
{
    public void ConfigureServices(IServiceCollection services)
    {
        services.AddApplication();
        services.AddDbContext<ITranslateDbContext, TranslateDbContext>(options =>
            options.UseInMemoryDatabase("ITranslateTrainerDb"));
        services.AddSingleton(TranslationSheetServiceMock.Get().Object);
    }
}
