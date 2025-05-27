using ExampleAPI.Converters;
using Newtonsoft.Json;

namespace ExampleAPI.Helpers;

public static class JsonDeserializer
{
    public static T DeserializeFromApi<T>(string json)
    {
        var settings = new JsonSerializerSettings
        {
            Converters = new List<JsonConverter> { new DateTimeBrasiliaConverter() },
        };

        return JsonConvert.DeserializeObject<T>(json, settings);
    }
}