using System.Runtime.Serialization;

using Application.Common.Mappings;
using Application.Domain.Entities;
using Application.Features.Todos;

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

    [Theory]
    [InlineData(typeof(TodoList), typeof(TodoListResponse))]
    [InlineData(typeof(TodoItem), typeof(TodoItemResponse))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

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