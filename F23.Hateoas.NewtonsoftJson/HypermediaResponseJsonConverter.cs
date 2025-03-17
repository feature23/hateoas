using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace F23.Hateoas.NewtonsoftJson;

public class HypermediaResponseJsonConverter : JsonConverter
{
    public override void WriteJson(JsonWriter writer, object? value, JsonSerializer serializer)
    {
        if (value is not HypermediaResponse response)
        {
            writer.WriteNull();
            return;
        }

        writer.WriteStartObject();

        writer.WritePropertyName("content");
        serializer.Serialize(writer, response.Content);

        if (response.Links != null)
        {
            writer.WritePropertyName("_links");
            writer.WriteStartArray();

            foreach (var link in response.Links)
            {
                serializer.Serialize(writer, link);
            }

            writer.WriteEndArray();
        }

        writer.WriteEndObject();
    }

    public override object ReadJson(JsonReader reader, Type objectType, object? existingValue, JsonSerializer serializer)
    {
        var jsonObject = JObject.Load(reader);

        var content = jsonObject["content"]
            ?? throw new JsonSerializationException("Missing required property 'content'.");

        var response = new HypermediaResponse(content);

        var links = jsonObject["_links"];

        if (links != null)
        {
            foreach (var link in links)
            {
                response.Add(serializer.Deserialize<HypermediaLink>(link.CreateReader())!);
            }
        }

        return response;
    }

    public override bool CanConvert(Type objectType)
        => objectType == typeof(HypermediaResponse);
}
