using Application.Common.Models;
using Application.Domain.Entities;
using Application.Domain.ValueObjects;
using Application.Infrastructure.Persistance;

using Carter;

using FluentValidation;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Todos;

public class CreateTodoListModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/api/todo-lists", async (CreateTodoListCommand request, IMediator mediator) =>
        {
            var response = await mediator.Send(request);
            return response;
        })
        .Produces<int>(StatusCodes.Status201Created)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Create a todo list",
            Description = "Create a todo list",
        });
    }
}

public record CreateTodoListCommand(string Title, string Colour) : IRequest<int>;

public class CreateTodoListCommandValidator : AbstractValidator<CreateTodoListCommand>
{
    public CreateTodoListCommandValidator()
    {
        RuleFor(i => i.Title).NotEmpty();
        RuleFor(i => i.Colour).Must(i => (Colour)i is not null);
    }
}

public class CreateTodoListCommandHandler(ApplicationDbContext context) : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var todoList = TodoList.Create(request.Title, (Colour)request.Colour);

        await _context.TodoLists.AddAsync(todoList, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return todoList.Id;
    }
}