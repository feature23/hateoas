using Newtonsoft.Json;

namespace F23.Hateoas.Tests;

public class NewtonsoftJsonSerializationTests
{
    [Fact]
    public void Serialize_WithNullLinks_ShouldNotIncludeLinks()
    {
        var response = new HypermediaResponse(42);

        var json = JsonConvert.SerializeObject(response);

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
                new HypermediaLink("google", "https://www.google.com"),
            }
        };

        var json = JsonConvert.SerializeObject(response);

        Assert.Equal("""{"content":42,"_links":[{"rel":"self","href":"/api/example"},{"rel":"google","href":"https://www.google.com"}]}""", json);
    }
}
