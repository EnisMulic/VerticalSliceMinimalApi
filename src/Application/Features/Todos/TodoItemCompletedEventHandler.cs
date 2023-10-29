using Application.Common.Behaviours;
using Application.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Features.Todos;

public class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogInformation($"ProjectNmae Domain Event: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}
