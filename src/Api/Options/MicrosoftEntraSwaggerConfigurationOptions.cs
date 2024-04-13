using Application.Authorization;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Options;

public class MicrosoftEntraSwaggerConfigurationOptions(IOptions<MicrosoftEntraOptions> options) : IConfigureOptions<SwaggerGenOptions>
{
    private readonly MicrosoftEntraOptions _options = options.Value;

    public void Configure(SwaggerGenOptions options)
    {
        var authorizationUrl = _options?.AuthorizationUrl ?? string.Empty;
        var tokenUrl = _options?.TokenUrl ?? string.Empty;
        var clientId = _options?.ClientId ?? string.Empty;

        options.AddSecurityDefinition("Entra", new OpenApiSecurityScheme
        {
            Type = SecuritySchemeType.OAuth2,
            Flows = new OpenApiOAuthFlows
            {
                AuthorizationCode = new OpenApiOAuthFlow
                {
                    AuthorizationUrl = new Uri(authorizationUrl),
                    TokenUrl = new Uri(tokenUrl),
                    Scopes = new[] { $"{AuthorizationScopes.AccessAsUser}" }
                        .ToDictionary(p => $"api://{clientId}/{p}")
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
                        Id = "Entra"
                    },
                    UnresolvedReference = true
                },
                Array.Empty<string>()
            }
        });
    }
}