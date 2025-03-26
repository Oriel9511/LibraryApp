using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper;
using LibraryApp.Application.Common.Interfaces;
using LibraryApp.Application.Common.Models.Authors;
using LibraryApp.Application.Common.Models.Books;
using LibraryApp.Application.Common.Models.Genres;
using LibraryApp.Domain.Entities;
using NUnit.Framework;

namespace LibraryApp.Application.UnitTests.Common.Mappings;

public class MappingTests
{
    private readonly IConfigurationProvider _configuration;
    private readonly IMapper _mapper;

    public MappingTests()
    {
        _configuration = new MapperConfiguration(config => 
            config.AddMaps(Assembly.GetAssembly(typeof(IApplicationDbContext))));

        _mapper = _configuration.CreateMapper();
    }

    [Test]
    public void ShouldHaveValidConfiguration()
    {
        _configuration.AssertConfigurationIsValid();
    }

    [Test]
    [TestCase(typeof(Book), typeof(BookDto))]
    [TestCase(typeof(Book), typeof(BookBasicDto))]
    [TestCase(typeof(Genre), typeof(GenreDto))]
    [TestCase(typeof(Genre), typeof(GenreBasicDto))]
    [TestCase(typeof(Author), typeof(AuthorDto))]
    [TestCase(typeof(Author), typeof(AuthorBasicDto))]
    public void ShouldSupportMappingFromSourceToDestination(Type source, Type destination)
    {
        var instance = GetInstanceOf(source);

        _mapper.Map(instance, source, destination);
    }

    private object GetInstanceOf(Type type)
    {
        if (type.GetConstructor(Type.EmptyTypes) != null)
            return Activator.CreateInstance(type)!;

        // Type without parameterless constructor
        return RuntimeHelpers.GetUninitializedObject(type);
    }
}
