using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests;

[TestFixture]
public abstract class BaseTestFixture
{
    [SetUp]
    public async Task TestSetup()
    {
        await ResetState();
    }
}