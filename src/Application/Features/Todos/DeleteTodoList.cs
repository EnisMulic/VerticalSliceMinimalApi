using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Domain.Entities;
using Application.Infrastructure.Persistance;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Todos;

public class DeleteTodoListModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/todo-lists/{id}", async (int id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTodoListCommand(id));

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Delete a todo list",
            Description = "Delete a todo list",
        });

    }
}

public record DeleteTodoListCommand(int Id) : IRequest;

public class DeleteTodoListCommandHandler : IRequestHandler<DeleteTodoListCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public DeleteTodoListCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(DeleteTodoListCommand request, CancellationToken cancellationToken)
    {
        var list = await _context.TodoLists.Include(i => i.Items)
            .FirstOrDefaultAsync(i => i.Id == request.Id, cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);

        _context.TodoLists.Remove(list);
        await _context.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}