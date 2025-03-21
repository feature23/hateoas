using F23.Hateoas.NewtonsoftJson;
using Newtonsoft.Json;

namespace F23.Hateoas.Tests;

public class NewtonsoftJsonSerializationTests
{
    private static readonly JsonSerializerSettings _settings = new JsonSerializerSettings
    {
        Converters =
        {
            new HypermediaResponseJsonConverter(),
            new HypermediaLinkJsonConverter(),
        }
    };

    [Fact]
    public void Serialize_WithNullLinks_ShouldNotIncludeLinks()
    {
        var response = new HypermediaResponse(42);

        var json = JsonConvert.SerializeObject(response, _settings);

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

        var json = JsonConvert.SerializeObject(response, _settings);

        Assert.Equal("""{"content":42,"_links":[{"rel":"self","href":"/api/example"},{"rel":"google","href":"https://www.google.com","method":"GET"}]}""", json);
    }
}
