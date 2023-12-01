using Application.Common.Behaviours;
using Application.Domain.Events;

using MediatR;

using Microsoft.Extensions.Logging;

namespace Application.Features.Todos;

public class TodoItemDeletedHandler : INotificationHandler<TodoItemDeletedEvent>
{
    private readonly ILogger<TodoItemDeletedHandler> _logger;

    public TodoItemDeletedHandler(ILogger<TodoItemDeletedHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemDeletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.LogNotificationInformation($"ProjectNmae Domain Event: {notification.GetType().Name}");

        return Task.CompletedTask;
    }
}