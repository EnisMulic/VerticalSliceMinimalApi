using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;
public class TodoItemDeletedEvent(TodoItem item) : DomainEvent
{
    public TodoItem Item { get; } = item;
}