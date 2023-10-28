using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;

public class TodoItemCompletedEvent : DomainEvent
{
    public TodoItem Item { get; }
    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }
}