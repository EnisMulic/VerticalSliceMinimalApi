using System.Data.Common;

using Application.Infrastructure.Persistance;

using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

using Respawn;

#if UseMsSql
using Testcontainers.MsSql;
#else
using Testcontainers.PostgreSql;
#endif

namespace Application.IntegrationTests;

public class TestContainersDatabase : ITestDatabase
{
#if UseMsSql
    private readonly MsSqlContainer _container;
#else
    private readonly PostgreSqlContainer _container;
#endif

    private DbConnection _connection = null!;
    private string _connectionString = null!;
    private Respawner _respawner = null!;

    public TestContainersDatabase()
    {
#if UseMsSql
        _container = new MsSqlBuilder()
            .WithAutoRemove(true)
            .Build();
#else
        _container = new PostgreSqlBuilder()
            .WithAutoRemove(true)
            .Build();
#endif
    }

    public async Task InitialiseAsync()
    {
        await _container.StartAsync();

        _connectionString = _container.GetConnectionString();

        _connection = new SqlConnection(_connectionString);

        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseSqlServer(_connectionString)
            .Options;

        var context = new ApplicationDbContext(options);

        context.Database.Migrate();

        _respawner = await Respawner.CreateAsync(_connectionString, new RespawnerOptions
        {
            TablesToIgnore = new Respawn.Graph.Table[] { "__EFMigrationsHistory" }
        });
    }

    public DbConnection GetConnection()
    {
        return _connection;
    }

    public async Task ResetAsync()
    {
        await _respawner.ResetAsync(_connectionString);
    }

    public async Task DisposeAsync()
    {
        await _connection.DisposeAsync();
        await _container.DisposeAsync();
    }
}