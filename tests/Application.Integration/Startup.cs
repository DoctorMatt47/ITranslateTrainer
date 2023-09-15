using ITranslateTrainer.Application.Common.Extensions;
using ITranslateTrainer.Application.Common.Interfaces;
using ITranslateTrainer.Infrastructure.Persistence.Contexts;
using ITranslateTrainer.Tests.Application.Integration.Common.Mocks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Tests.Application.Integration;

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
