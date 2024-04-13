using Microsoft.OpenApi.Models;

namespace Application.Common.Models;

public static class OpenApiTags
{
    public static List<OpenApiTag> TodoList { get; } = [new OpenApiTag { Name = "Todo Lists" }];
}