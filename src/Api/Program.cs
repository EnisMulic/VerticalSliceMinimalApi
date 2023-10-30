// CA1852 Type 'Program' can be sealed because it has no subtypes in its containing assembly and is not externally visible
#pragma warning disable CA1852

using Api;
using Api.Extensions;
using Api.Options;
using Api.Services;

using Application;
using Application.Infrastructure;
using Application.Infrastructure.Persistance;

using Carter;

using Hellang.Middleware.ProblemDetails;

using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
// Register the Swagger generator, defining 1 or more Swagger documents
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSwaggerGen(options => options.SwaggerDoc("v1", new OpenApiInfo
{
    Version = "v1",
    Title = "ProjectName API",
    Description = "<API Description Placeholder>",
}));


//builder.Services.ConfigureSwaggerGen(options => options.CustomSchemaIds(x => x.FullName));

builder.Services.AddCors(options => options.AddDefaultPolicy(
    policy => policy.AllowAnyOrigin()
        .AllowAnyHeader()
        .AllowAnyMethod()));


builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, SwaggerConfigurationOptions>();

builder.Services.AddAndConfigureProblemDetails();

builder.Services.AddAuthorization();
builder.Services.AddAuthentication().AddJwtBearer();


builder.Services.AddApiServices();
builder.Services.AddCommonServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddCarter();

var app = builder.Build();

app.MapHealthChecks("/api/health", new HealthCheckOptions
{
    ResponseWriter = HealthChecksResponseWriter.WriteResponse
});

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    // if environment is Development, then migrate the database
    if (app.Environment.IsDevelopment())
    {
        db.Database.Migrate();
    }
}

// Enable middleware to serve generated Swagger as a JSON endpoint.
app.UseSwagger();

// Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
app.UseSwaggerUI(options =>
{
    options.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
    options.RoutePrefix = string.Empty;
});

app.UseCors();

app.UseProblemDetails();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllers();
app.MapCarter();

app.Run();