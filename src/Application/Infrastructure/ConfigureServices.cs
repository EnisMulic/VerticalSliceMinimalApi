using Application.Common.Interfaces;
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Persistance.Interceptors;
using Application.Infrastructure.Services;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<AuditableEntitySaveChangesInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

            options.UseSqlServer(configuration.GetConnectionString("Default"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
        });

        services.AddTransient<IDateTime, DateTimeService>();

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("Database");


        return services;
    }
}