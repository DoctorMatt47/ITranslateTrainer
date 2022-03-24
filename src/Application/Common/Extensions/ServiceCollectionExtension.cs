using System.Reflection;
using ITranslateTrainer.Application.Common.Behaviours;
using ITranslateTrainer.Application.Common.Responses;
using ITranslateTrainer.Application.Texts.Commands;
using ITranslateTrainer.Application.Texts.Requests;
using ITranslateTrainer.Application.Translations.Commands;
using ITranslateTrainer.Application.Translations.Requests;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());

        services.AddScoped(typeof(IPipelineBehavior<,>), typeof(TransactionSaveBehaviour<,>));

        services.AddScoped(typeof(IPipelineBehavior<FilterText, string>),
            typeof(FilterTextValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<PatchTextCommand, Unit>),
            typeof(PatchTextCommandValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<CreateTranslationCommand, IntIdResponse>),
            typeof(CreateTranslationValidateBehaviour));
        services.AddScoped(typeof(IPipelineBehavior<DeleteTranslationCommand, Unit>),
            typeof(DeleteTranslationCommandValidateBehaviour));

        return services;
    }
}