using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;

public class TodoItemCompletedEvent(TodoItem item) : DomainEvent
{
    public TodoItem Item { get; } = item;
}