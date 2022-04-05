using System.Reflection;
using ITranslateTrainer.Application.Common.Behaviours;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Application.Texts.Extensions;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Handlers;
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

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(CommandSaveBehaviour<,>));

        services.AddScoped(typeof(IPipelineBehavior<FilterText, string>),
            typeof(FilterTextValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<PatchTextCommand, Unit>),
            typeof(PatchTextCommandValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<CreateTranslation, Translation>),
            typeof(CreateTranslationValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<DeleteTranslationCommand, Unit>),
            typeof(DeleteTranslationCommandValidateBehaviour));

        return services;
    }
}
