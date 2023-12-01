using Application.Common.Behaviours;
using Application.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Features.Todos;

public class TodoItemCompletedHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedHandler> _logger;

    public TodoItemCompletedHandler(ILogger<TodoItemCompletedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"ProjectNmae Domain Event: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}