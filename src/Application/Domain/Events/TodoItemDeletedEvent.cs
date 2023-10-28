using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;
public class TodoItemDeletedEvent : DomainEvent
{
    public TodoItem Item { get; }
    public TodoItemDeletedEvent(TodoItem item)
    {
        Item = item;
    }
}