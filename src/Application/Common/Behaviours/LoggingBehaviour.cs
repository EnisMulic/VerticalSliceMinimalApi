using Application.Common.Interfaces;

using MediatR.Pipeline;

using Microsoft.Extensions.Logging;

namespace Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger<TRequest> _logger;
        private readonly ICurrentUserService _currentUserService;

        public LoggingBehaviour(ILogger<TRequest> logger, ICurrentUserService currentUserService)
        {
            _logger = logger;
            _currentUserService = currentUserService;
        }

        public Task Process(TRequest request, CancellationToken cancellationToken)
        {
            var requestName = typeof(TRequest).Name;
            var userEmail = _currentUserService.Email ?? string.Empty;

            _logger.LogApiRequest($"ProjectName IRequest:[{requestName}], UserEmail:[{userEmail}], Data:[{request}]");

            return Task.CompletedTask;
        }
    }
}