using System.Reflection;

using Application.Common.Behaviours;

using FluentValidation;

using MediatR;

using Microsoft.Extensions.DependencyInjection;

namespace Application;

public static class ConfigureCommonServices
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddMediatR(Assembly.GetExecutingAssembly());
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));
        services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PerformanceBehaviour<,>));

        return services;
    }
}