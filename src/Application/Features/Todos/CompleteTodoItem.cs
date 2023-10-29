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

public class CompleteTodoItemModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPatch("/api/todos/{id}", async (int id, IMediator mediator) =>
        {
            await mediator.Send(new CompleteTodoCommand(id));

            return Results.NoContent();
        })
        .Produces(StatusCodes.Status204NoContent)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Complete a todo item",
            Description = "Complete a todo item",
        });
    }
}

public record CompleteTodoCommand(int Id) : IRequest;

public class CompleteTodoCommandHandler : IRequestHandler<CompleteTodoCommand, Unit>
{
    private readonly ApplicationDbContext _context;

    public CompleteTodoCommandHandler(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Unit> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.TodoItems.FindAsync(new object?[] { request.Id }, cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);

        //if (!item.Done)
        //{
            
        //}

        item.Complete();
        await _context.SaveChangesAsync(cancellationToken);

        return Unit.Value;
    }
}
