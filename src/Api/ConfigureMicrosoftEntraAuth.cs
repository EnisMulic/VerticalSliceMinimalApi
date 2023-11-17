using Api.Options;

using Microsoft.Extensions.Options;
using Microsoft.Identity.Web;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api;

public static class ConfigureMicrosoftEntraAuth
{
    public static IServiceCollection AddMicrosoftEntraAuth(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<MicrosoftEntraOptions>(options =>
            configuration.Bind(MicrosoftEntraOptions.SectionName, options));

        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, MicrosoftEntraSwaggerConfigurationOptions>();

        services.AddAuthentication()
            .AddMicrosoftIdentityWebApi(configuration, MicrosoftEntraOptions.SectionName)
                .EnableTokenAcquisitionToCallDownstreamApi()
                .AddInMemoryTokenCaches();

        return services;
    }
}