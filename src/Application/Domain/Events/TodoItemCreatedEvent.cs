using Application.Domain.Common;
using Application.Domain.Entities;

namespace Application.Domain.Events;

public class TodoItemCreatedEvent(TodoItem item) : DomainEvent
{
    public TodoItem Item { get; } = item;
}