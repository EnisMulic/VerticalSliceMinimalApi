using Application.Common.Interfaces;
#if UseDatabase
using Application.Infrastructure.Persistance;
using Application.Infrastructure.Persistance.Interceptors;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

#endif
using Application.Infrastructure.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Infrastructure;

public static class ConfigureInfrastructureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddTransient<IDateTime, DateTimeService>();

#if UseDatabase
        services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, DispatchDomainEventsInterceptor>();
        services.AddScoped<ISaveChangesInterceptor, SoftDeleteInterceptor>();

        services.AddDbContext<ApplicationDbContext>((sp, options) =>
        {
            options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());

#if UseMsSql
            options.UseSqlServer(configuration.GetConnectionString("Default"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
#elif UsePostgreSql
            options.UseNpgsql(configuration.GetConnectionString("Default"),
                builder => builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
#endif
        });

        services.AddHealthChecks()
            .AddDbContextCheck<ApplicationDbContext>("Database");
#endif

        return services;
    }
}