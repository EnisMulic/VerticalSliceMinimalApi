using Application.Common.Exceptions;
using Application.Domain.Entities;
using Application.Domain.Exceptions;
using Application.Features.Todos;

using static Application.IntegrationTests.Testing;

namespace Application.IntegrationTests.Features.Todos;
public class CreateTodoListCommandTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireMinimumFields()
    {
        var command = new CreateTodoListCommand("", "");

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<ValidationException>();
    }

    [Test]
    public async Task ShouldRequireSupportedColour()
    {
        var command = new CreateTodoListCommand("Tasks", "");

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<UnsupportedColourException>();
    }


    [Test]
    public async Task ShouldCreateTodoList()
    {
        var command = new CreateTodoListCommand("Tasks", "#FFFFFF");

        var id = await SendAsync(command);

        var list = await FindAsync<TodoList>(id);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.Title);
        list!.Colour.Should().Be(command.Colour);
        list.Created.Should().BeCloseTo(DateTime.Now, TimeSpan.FromMilliseconds(10000));
    }
}
