using System.Text.Json;

namespace F23.Hateoas.Tests;

public class HypermediaResponseTests
{
    [Fact]
    public void Constructor_WithNonNullContent_ShouldSetContent()
    {
        // Arrange
        var content = new { Name = "Test", Value = 42 };

        // Act
        var response = new HypermediaResponse(content);

        // Assert
        Assert.Equal(content, response.Content);
    }

    [Fact]
    public void Constructor_WithNullContent_ShouldSetContentToNull()
    {
        // Arrange & Act
        var response = new HypermediaResponse(null);

        // Assert
        Assert.Null(response.Content);
    }

    [Fact]
    public void Constructor_WithStringContent_ShouldSetContent()
    {
        // Arrange
        var content = "Hello, World!";

        // Act
        var response = new HypermediaResponse(content);

        // Assert
        Assert.Equal(content, response.Content);
    }

    [Fact]
    public void Constructor_WithPrimitiveContent_ShouldSetContent()
    {
        // Arrange
        var content = 123;

        // Act
        var response = new HypermediaResponse(content);

        // Assert
        Assert.Equal(content, response.Content);
    }

    [Fact]
    public void Links_InitiallyNull_ShouldBeNull()
    {
        // Arrange & Act
        var response = new HypermediaResponse("test");

        // Assert
        Assert.Null(response.Links);
    }

    [Fact]
    public void Links_CanBeSetDirectly()
    {
        // Arrange
        var response = new HypermediaResponse("test");
        var links = new List<HypermediaLink>
        {
            new HypermediaLink("self", "/api/test")
        };

        // Act
        response.Links = links;

        // Assert
        Assert.Equal(links, response.Links);
        Assert.Single(response.Links);
    }

    [Fact]
    public void Add_WithNullLinks_ShouldInitializeLinksAndAddLink()
    {
        // Arrange
        var response = new HypermediaResponse("test");
        var link = new HypermediaLink("self", "/api/test");

        // Act
        response.Add(link);

        // Assert
        Assert.NotNull(response.Links);
        Assert.Single(response.Links);
        Assert.Equal(link, response.Links[0]);
    }

    [Fact]
    public void Add_WithExistingLinks_ShouldAppendLink()
    {
        // Arrange
        var response = new HypermediaResponse("test");
        var firstLink = new HypermediaLink("self", "/api/test");
        var secondLink = new HypermediaLink("next", "/api/test/next");

        // Act
        response.Add(firstLink);
        response.Add(secondLink);

        // Assert
        Assert.NotNull(response.Links);
        Assert.Equal(2, response.Links.Count);
        Assert.Equal(firstLink, response.Links[0]);
        Assert.Equal(secondLink, response.Links[1]);
    }

    [Fact]
    public void Add_MultipleLinks_ShouldMaintainOrder()
    {
        // Arrange
        var response = new HypermediaResponse("test");
        var links = new[]
        {
            new HypermediaLink("self", "/api/test"),
            new HypermediaLink("edit", "/api/test/edit"),
            new HypermediaLink("delete", "/api/test/delete")
        };

        // Act
        foreach (var link in links)
        {
            response.Add(link);
        }

        // Assert
        Assert.NotNull(response.Links);
        Assert.Equal(3, response.Links.Count);
        for (int i = 0; i < links.Length; i++)
        {
            Assert.Equal(links[i], response.Links[i]);
        }
    }

    [Fact]
    public void Serialize_WithNullContent_ShouldSerializeNull()
    {
        // Arrange
        var response = new HypermediaResponse(null);

        // Act
        var json = JsonSerializer.Serialize(response);

        // Assert
        Assert.Equal("""{"content":null}""", json);
    }

    [Fact]
    public void Serialize_WithNullContentAndLinks_ShouldSerializeNullContentAndIncludeLinks()
    {
        // Arrange
        var response = new HypermediaResponse(null);
        response.Add(new HypermediaLink("self", "/api/test"));

        // Act
        var json = JsonSerializer.Serialize(response);

        // Assert
        Assert.Equal("""{"content":null,"_links":[{"rel":"self","href":"/api/test"}]}""", json);
    }

    [Fact]
    public void Serialize_WithComplexObjectContent_ShouldSerializeCorrectly()
    {
        // Arrange
        var content = new { Id = 1, Name = "Test Item", Active = true };
        var response = new HypermediaResponse(content);

        // Act
        var json = JsonSerializer.Serialize(response);

        // Assert
        Assert.Equal("""{"content":{"Id":1,"Name":"Test Item","Active":true}}""", json);
    }

    [Fact]
    public void Serialize_WithArrayContent_ShouldSerializeCorrectly()
    {
        // Arrange
        var content = new[] { 1, 2, 3 };
        var response = new HypermediaResponse(content);

        // Act
        var json = JsonSerializer.Serialize(response);

        // Assert
        Assert.Equal("""{"content":[1,2,3]}""", json);
    }
}
