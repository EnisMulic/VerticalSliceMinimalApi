using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Application.Common.Models;
using Application.Domain.Entities;
using Application.Domain.Enums;
using Application.Infrastructure.Persistance;

using Carter;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Todos;

public class CreateTodoItemModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/todo-lists/{id}", async ([FromRoute] int id, CreateTodoItemRequest request, IMediator mediator) =>
        {
            var command = new CreeateTodoItemCommand(id, request);
            var response = await mediator.Send(command);

            return response;
        })
        .Produces<int>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Create a todo item",
            Description = "Create a todo item",
        });
    }
}

public record CreateTodoItemRequest(string Title, string? Note, PriorityLevel PriorityLevel, DateTime? Reminder);

public record CreeateTodoItemCommand(int Id, CreateTodoItemRequest Item) : IRequest<int>;

public class CreeateTodoItemCommandValidator : AbstractValidator<CreeateTodoItemCommand>
{
    private readonly IDateTime _dateTime;

    public CreeateTodoItemCommandValidator(IDateTime dateTime)
    {
        _dateTime = dateTime;

        RuleFor(i => i.Item.Title).NotEmpty();
        RuleFor(i => i.Item.Reminder).GreaterThan(_dateTime.Now);
    }
}

public class CreeateTodoItemCommandHandler : IRequestHandler<CreeateTodoItemCommand, int>
{
    private readonly ApplicationDbContext _context;

    public CreeateTodoItemCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreeateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var todoList = await _context.TodoLists.FindAsync(new object?[] { request.Id }, cancellationToken)
            ?? throw new NotFoundException(nameof(TodoList), request.Id);

        var todoItem = TodoItem.Create(request.Item.Title,
            request.Item.Note,
            request.Item.PriorityLevel,
            request.Item.Reminder);

        todoList.AddTodoItem(todoItem);

        await _context.SaveChangesAsync(cancellationToken);

        return todoItem.Id;
    }
}