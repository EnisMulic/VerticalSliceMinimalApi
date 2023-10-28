using Application.Common.Exceptions;
using Application.Domain.Exceptions;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Mvc;

using ProblemDetailsOptions = Hellang.Middleware.ProblemDetails.ProblemDetailsOptions;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Api.Extensions;

public static class ProblemDetailsExtension
{
    public static IServiceCollection AddAndConfigureProblemDetails(this IServiceCollection services)
    {
        services.AddProblemDetails((ProblemDetailsOptions options) =>
        {
            options.IncludeExceptionDetails = (ctx, ex) => Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development";

            options.Map<ValidationException>(ex => new ValidationProblemDetails(ex.Errors)
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.1",
                Status = StatusCodes.Status400BadRequest
            });

            options.Map<NotFoundException>(ex => new ProblemDetails
            {
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.4",
                Title = "The specified resource was not found.",
                Detail = ex.Message,
                Status = StatusCodes.Status404NotFound
            });

            options.Map<UnauthorizedAccessException>(ex => new ProblemDetails
            {
                Status = StatusCodes.Status401Unauthorized,
                Title = "Unauthorized",
                Type = "https://tools.ietf.org/html/rfc7235#section-3.1"
            });

            options.Map<ForbiddenAccessException>(ex => new ProblemDetails
            {
                Status = StatusCodes.Status403Forbidden,
                Title = "Forbidden",
                Type = "https://tools.ietf.org/html/rfc7231#section-6.5.3"
            });

            options.MapToStatusCode<UnsupportedColourException>(StatusCodes.Status400BadRequest);

            options.MapToStatusCode<ArgumentException>(StatusCodes.Status400BadRequest);
            options.MapToStatusCode<ArgumentNullException>(StatusCodes.Status400BadRequest);

            options.MapToStatusCode<NotImplementedException>(StatusCodes.Status501NotImplemented);
            options.MapToStatusCode<Exception>(StatusCodes.Status500InternalServerError);
        });

        return services;
    }
}