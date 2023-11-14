using System.Diagnostics;
using System.Text.Json;

using Application.Common.Interfaces;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours;

/// <summary>
/// Logging long running (>3000ms) MediatR requests
/// </summary>
/// <typeparam name="TRequest"></typeparam>
/// <typeparam name="TResponse"></typeparam>
public class PerformanceBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly Stopwatch _timer;
    private readonly ILogger<TRequest> _logger;
    private readonly ICurrentUserService _currentUserService;

    public PerformanceBehaviour(
        ILogger<TRequest> logger,
        ICurrentUserService currentUserService)
    {
        _timer = new Stopwatch();

        _logger = logger;
        _currentUserService = currentUserService;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        _timer.Start();

        var response = await next().ConfigureAwait(false);

        _timer.Stop();

        var elapsedMilliseconds = _timer.ElapsedMilliseconds;

        if (elapsedMilliseconds > 3000)
        {
            var requestName = typeof(TRequest).Name;
            var userEmail = _currentUserService.Email;

            _logger.LogPerformanceWarning(
                $"ProjectName long running IRequest:[{requestName}] ({elapsedMilliseconds} milliseconds), RequestBody: {JsonSerializer.Serialize(request)}, UserEmail: {userEmail}");
        }

        return response;
    }
}