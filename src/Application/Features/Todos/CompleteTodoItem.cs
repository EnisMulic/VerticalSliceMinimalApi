using Application.Common.Exceptions;
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
        .WithTags(OpenApiTags.TodoList);
    }
}

public record CompleteTodoCommand(int Id) : IRequest;

public class CompleteTodoCommandHandler(ApplicationDbContext context) : IRequestHandler<CompleteTodoCommand, Unit>
{
    private readonly ApplicationDbContext _context = context;

    public async Task<Unit> Handle(CompleteTodoCommand request, CancellationToken cancellationToken)
    {
        var item = await _context.TodoItems.FindAsync([request.Id], cancellationToken: cancellationToken)
            ?? throw new NotFoundException(nameof(TodoItem), request.Id);

        if (!item.Done)
        {
            item.Complete();
            await _context.SaveChangesAsync(cancellationToken);
        }

        return Unit.Value;
    }
}