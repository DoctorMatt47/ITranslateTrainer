using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence.Contexts;
using ITranslateTrainer.IntegrationTests.Application.Common.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.IntegrationTests.Application;

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
