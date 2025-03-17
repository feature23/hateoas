using System.Diagnostics.CodeAnalysis;
using System.Text.Json.Serialization;

namespace F23.Hateoas;

public class HypermediaLink
{
    public HypermediaLink()
    {
    }

    [SetsRequiredMembers]
    public HypermediaLink(string rel, string href, string? method = null)
    {
        Rel = rel;
        Href = href;
        Method = method;
    }

    [JsonPropertyName("rel")]
    public required string Rel { get; init; }

    [JsonPropertyName("href")]
    public required string Href { get; init; }

    [JsonPropertyName("method")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public string? Method { get; set; }
}
