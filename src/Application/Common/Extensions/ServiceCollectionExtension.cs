using System.Reflection;
using ITranslateTrainer.Application.Texts;
using ITranslateTrainer.Application.Translations;
using ITranslateTrainer.Domain.Entities;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped(
            typeof(IPipelineBehavior<PatchTextCommand, Unit>),
            typeof(PatchTextCommandValidateBehaviour));

        services.AddScoped(
            typeof(IPipelineBehavior<GetOrCreateTranslation, Translation>),
            typeof(GetOrCreateTranslationRequestValidateBehaviour));

        services.AddScoped(
            typeof(IPipelineBehavior<DeleteTranslationCommand, Unit>),
            typeof(DeleteTranslationCommandValidateBehaviour));

        return services;
    }
}
