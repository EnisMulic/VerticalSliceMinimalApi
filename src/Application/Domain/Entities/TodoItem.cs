using Application.Domain.Common;
using Application.Domain.Enums;

namespace Application.Domain.Entities;

public class TodoItem : BaseAuditableEntity, IHasDomainEvent
{
    public string? Title { get; private set; }
    public string? Note { get; private set; }
    public PriorityLevel PriorityLevel { get; private set; }
    public bool Done { get; private set; }
    public DateTime? Reminder { get; private set; }
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
        return new(title, note, priorityLevel, reminder);
    }
}