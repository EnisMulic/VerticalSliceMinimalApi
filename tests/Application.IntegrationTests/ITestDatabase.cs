using System.Data.Common;

namespace Application.IntegrationTests;

public interface ITestDatabase
{
    Task InitialiseAsync();
    DbConnection GetConnection();
    Task ResetAsync();
    Task DisposeAsync();
}