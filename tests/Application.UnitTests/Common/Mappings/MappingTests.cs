using System.Runtime.Serialization;

using Application.Common.Mappings;

using AutoMapper;

namespace Application.UnitTests.Common.Mappings;

public class MappingTestsFixture
{
    public MappingTestsFixture()
    {
        ConfigurationProvider = new MapperConfiguration(cfg => cfg.AddProfile<MappingProfile>());

        Mapper = ConfigurationProvider.CreateMapper();
    }

    public IConfigurationProvider ConfigurationProvider { get; }

    public IMapper Mapper { get; }
}

public class MappingTests : IClassFixture<MappingTestsFixture>
{
    private readonly IConfigurationProvider _configuration;
#pragma warning disable IDE0052
    private readonly IMapper _mapper;
#pragma warning restore IDE0052

    public MappingTests(MappingTestsFixture fixture)
    {
        _configuration = fixture.ConfigurationProvider;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    // TODO: left commented on purpose, so we have an example of test method for IMapper mappings
    // [Theory]
    // [InlineData(typeof(TodoList), typeof(TodoListDto))]
    // [InlineData(typeof(TodoItem), typeof(TodoItemDto))]
    // public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    // {
    //     var instance = GetInstanceOf(source);
    //
    //     _mapper.Map(instance, source, destination);
    // }

    private object? GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
        {
            return Activator.CreateInstance(type);
        }

        // Type without parameterless constructor
        return FormatterServices.GetUninitializedObject(type);
    }
}