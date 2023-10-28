using Api.Services;

using Application.Common.Interfaces;

using Microsoft.AspNetCore.Mvc;

namespace Api;

public static class ConfigureApiServices
{
    public static IServiceCollection AddApiServices(this IServiceCollection services, ConfigurationManager configuration)
    {
        services.AddSingleton<ICurrentUserService, CurrentUserService>();

        services.AddHttpContextAccessor();

        services.Configure<ApiBehaviorOptions>(options =>
            options.SuppressModelStateInvalidFilter = true);

        return services;
    }
}