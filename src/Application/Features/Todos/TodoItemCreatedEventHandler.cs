
using Application.Common.Behaviours;
using Application.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Features.Todos;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogNotificationInformation($"ProjectNmae Domain Event: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}
