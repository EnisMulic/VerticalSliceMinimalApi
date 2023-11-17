using Application.Common.Mappings;
using Application.Domain.Entities;
using Application.Domain.Enums;

namespace Application.Features.Todos;

#nullable disable
public class TodoListResponse : IMapFrom<TodoList>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Colour { get; set; }
    public IList<TodoItemResponse> Items { get; set; }
}

public class TodoItemResponse : IMapFrom<TodoItem>
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Note { get; set; }
    public PriorityLevel PriorityLevel { get; set; }
    public bool Done { get; set; }
    public DateTime? Reminder { get; set; }
}