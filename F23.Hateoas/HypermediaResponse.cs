using System.Text.Json.Serialization;

namespace F23.Hateoas;

public class HypermediaResponse(object content)
{
    [JsonPropertyName("content")]
    [JsonIgnore(Condition = JsonIgnoreCondition.Never)]
    public object Content { get; } = content;

    [JsonPropertyName("_links")]
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public IList<HypermediaLink>? Links { get; set; }

    public void Add(HypermediaLink link)
    {
        Links ??= new List<HypermediaLink>();
        Links.Add(link);
    }
}
