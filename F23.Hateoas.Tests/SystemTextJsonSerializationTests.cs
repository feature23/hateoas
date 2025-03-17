using System.Text.Json;

namespace F23.Hateoas.Tests;

public class SystemTextJsonSerializationTests
{
    [Fact]
    public void Serialize_WithNullLinks_ShouldNotIncludeLinks()
    {
        var response = new HypermediaResponse(42);

        var json = JsonSerializer.Serialize(response);

        Assert.Equal("""{"content":42}""", json);
    }

    [Fact]
    public void Serialize_WithLinks_ShouldIncludeLinks()
    {
        var response = new HypermediaResponse(42)
        {
            Links = new List<HypermediaLink>
            {
                new HypermediaLink("self", "/api/example"),
                new HypermediaLink("google", "https://www.google.com", "GET"),
            }
        };

        var json = JsonSerializer.Serialize(response);

        Assert.Equal("""{"content":42,"_links":[{"rel":"self","href":"/api/example"},{"rel":"google","href":"https://www.google.com","method":"GET"}]}""", json);
    }
}
