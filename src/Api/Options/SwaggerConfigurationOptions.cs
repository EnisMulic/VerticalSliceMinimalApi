using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Options;

public class SwaggerConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IConfiguration _configuration;

    public SwaggerConfigurationOptions(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void Configure(SwaggerGenOptions options)
    {

        options.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                }
            }
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "aad-jwt"
                    },
                    UnresolvedReference = true
                },
                Array.Empty<string>()
            }
        });
    }
}