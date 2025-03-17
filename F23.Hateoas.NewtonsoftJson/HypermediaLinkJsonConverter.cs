using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace F23.Hateoas.NewtonsoftJson;

public class HypermediaLinkJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        var response = (HypermediaLink)value!;

        writer.WriteStartObject();

        writer.WritePropertyName("rel");
        serializer.Serialize(writer, response.Rel);

        writer.WritePropertyName("href");
        serializer.Serialize(writer, response.Href);

        if (response.Method != null)
        {
            writer.WritePropertyName("method");
            serializer.Serialize(writer, response.Method);
        }

        writer.WriteEndObject();
    }

    public override object? ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var rel = jsonObject["rel"]
            ?? throw new JsonSerializationException("Missing required property 'rel'.");

        var href = jsonObject["href"]
            ?? throw new JsonSerializationException("Missing required property 'href'.");

        var method = jsonObject["method"];

        return new HypermediaLink(rel.Value<string>()!, href.Value<string>()!, method?.Value<string>());
    }

    public override bool CanConvert(Type objectType)
        => objectType == typeof(HypermediaLink);
}
