using System.Reflection;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace ITranslateTrainer.Application.Common.Extensions;

public static class ServiceCollectionExtension
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        return services;
    }
}