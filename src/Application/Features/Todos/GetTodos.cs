using Application.Common.Models;
using Application.Infrastructure.Persistance;

using AutoMapper;
using AutoMapper.QueryableExtensions;

using Carter;

using MediatR;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Todos;

public class GetTodosModule : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/api/todos", async (IMediator mediator) =>
        {
            var response = await mediator.Send(new GetTodosQuery());
            return response;
        })
        .RequireAuthorization()
        .Produces<List<TodoListResponse>>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status400BadRequest)
        .WithOpenApi(operation => new(operation)
        {
            Tags = OpenApiTags.TodoList,
            Summary = "Get todos",
            Description = "Get todos",
        });
    }
}

public record GetTodosQuery : IRequest<List<TodoListResponse>>;

public class GetTodosQueryHandler : IRequestHandler<GetTodosQuery, List<TodoListResponse>>
{
    private readonly ApplicationDbContext _context;
    private readonly IMapper _mapper;

    public GetTodosQueryHandler(ApplicationDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<TodoListResponse>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var list = await _context.TodoLists.Include(i => i.Items)
            .AsNoTracking()
            .ProjectTo<TodoListResponse>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);

        return list;
    }
}