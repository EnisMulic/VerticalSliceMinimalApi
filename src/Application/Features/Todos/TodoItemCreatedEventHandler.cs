
using Application.Common.Behaviours;
using Application.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Features.Todos;

public class TodoItemCreatedHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedHandler> _logger;

    public TodoItemCreatedHandler(ILogger<TodoItemCreatedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogNotificationInformation($"ProjectNmae Domain Event: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}