using Application.Common.Mappings;
using Application.Domain.Entities;
using Application.Domain.Enums;

namespace Application.Features.Todos;

#nullable disable
public class TodoListResponse : IMapFrom<TodoList>
{
    public string Title { get; set; }
    public string Colour { get; set; }
    public IList<TodoItemResponse> Items { get; set; }
}

public class TodoItemResponse : IMapFrom<TodoItem>
{
    public string Title { get; private set; }
    public string Note { get; private set; }
    public PriorityLevel PriorityLevel { get; private set; }
    public bool Done { get; private set; }
    public DateTime? Reminder { get; private set; }
}