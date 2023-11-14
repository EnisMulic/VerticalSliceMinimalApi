using Application.Domain.Common;
using Application.Domain.ValueObjects;

namespace Application.Domain.Entities;

#nullable disable
public class TodoList : BaseAuditableEntity
{
    public string Title { get; private set; }
    public Colour Colour { get; private set; }
    public IList<TodoItem> Items { get; private set; } = new List<TodoItem>();

    private TodoList() { }

    private TodoList(string title, Colour colour)
    {
        Title = title;
        Colour = colour;
        Items = new List<TodoItem>();
    }

    public static TodoList Create(string title, Colour colour)
    {
        return new(title, colour);
    }

    public void AddTodoItem(TodoItem item)
    {
        Items.Add(item);
    }
}