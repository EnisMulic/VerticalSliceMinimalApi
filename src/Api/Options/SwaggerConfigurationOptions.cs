using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

namespace Api.Options;

public class SwaggerConfigurationOptions : IConfigureOptions<SwaggerGenOptions>
{
    public SwaggerConfigurationOptions()
    {
    }

    public void Configure(SwaggerGenOptions options)
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "JWT Authorization header using the Bearer scheme."
        });
        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        //options.AddSecurityDefinition("aad-jwt", new OpenApiSecurityScheme
        //{
        //    Type = SecuritySchemeType.OAuth2,
        //    Flows = new OpenApiOAuthFlows
        //    {
        //        AuthorizationCode = new OpenApiOAuthFlow
        //        {
        //        }
        //    }
        //});
        //options.AddSecurityRequirement(new OpenApiSecurityRequirement
        //{
        //    {
        //        new OpenApiSecurityScheme
        //        {
        //            Reference = new OpenApiReference
        //            {
        //                Type = ReferenceType.SecurityScheme,
        //                Id = "aad-jwt"
        //            },
        //            UnresolvedReference = true
        //        },
        //        Array.Empty<string>()
        //    }
        //});
    }
}