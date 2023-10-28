using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;

public class TodoItemCreatedEvent : DomainEvent
{
    public TodoItem Item { get; }
    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }
}