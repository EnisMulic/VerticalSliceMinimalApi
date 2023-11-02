namespace Application.IntegrationTests;

public static class TestDatabaseFactory
{
    public static async Task<ITestDatabase> CreateAsync()
    {
        var database = new TestContainersDatabase();

        await database.InitialiseAsync();

        return database;
    }
}