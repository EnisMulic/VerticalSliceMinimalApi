using Application.Authorization;
using Application.Common.Exceptions;
using Application.Common.Models;
using Application.Domain.Entities;
using Application.Infrastructure.Persistance;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;

namespace Application.Features.Todos;

public class DeleteTodoItemModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/api/todos/{id}", async (int id, IMediator mediator) =>
        {
            await mediator.Send(new DeleteTodoItemCommand(id));

            return Results.NoContent();
        })
        .RequireAuthorization(policy => policy.RequireRole(Roles.Administrator))
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Delete a todo item",
            Description = "Delete a todo item",
        });
    }
}

public record DeleteTodoItemCommand(int Id) : IRequest;

public class DeleteTodoItemCommandHandler(ApplicationDbContext context) : IRequestHandler<DeleteTodoItemCommand, Unit>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Unit> Handle(DeleteTodoItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.TodoItems.FindAsync([request.Id], cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);

        _context.TodoItems.Remove(item);
        await _context.SaveChangesAsync(cancellationToken: cancellationToken);

        return Unit.Value;
    }
}