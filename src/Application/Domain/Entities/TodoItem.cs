using Application.Domain.Common;
using Application.Domain.Enums;
using Application.Domain.Events;

namespace Application.Domain.Entities;

public class TodoItem : BaseAuditableEntity, IHasDomainEvent, ISoftDelete
{
    public string? Title { get; private set; }
    public string? Note { get; private set; }
    public PriorityLevel PriorityLevel { get; private set; }
    public bool Done { get; private set; }
    public DateTime? Reminder { get; private set; }
    public bool IsDeleted { get; set; }
    public int TodoListId { get; private set; }
    public TodoList TodoList { get; private set; } = null!;

    public List<DomainEvent> DomainEvents { get; private set; } = new List<DomainEvent>();

    private TodoItem() { }

    private TodoItem(string title, string? note, PriorityLevel priorityLevel, DateTime? reminder)
    {
        Title = title;
        Note = note;
        PriorityLevel = priorityLevel;
        Reminder = reminder;
        Done = false;
    }

    public static TodoItem Create(string title, string? note, PriorityLevel priorityLevel, DateTime? reminder)
    {
        var item = new TodoItem(title, note, priorityLevel, reminder);

        var @event = new TodoItemCreatedEvent(item);
        item.DomainEvents.Add(@event);

        return item;
    }

    public void Complete()
    {
        Done = true;
        DomainEvents.Add(new TodoItemCompletedEvent(this));
    }

    public void Delete()
    {
        IsDeleted = true;
        DomainEvents.Add(new TodoItemDeletedEvent(this));
    }
}