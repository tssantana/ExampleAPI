using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace ExampleAPI.Converters;

public sealed class DateTimeBrasiliaConverter : DateTimeConverterBase
{
    public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
    {
        if (value is DateTime dateTime)
        {
            writer.WriteValue(dateTime.ToUniversalTime());
        }   
    }

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        if (reader.TokenType == JsonToken.Null)
        {
            return null;
        }

        if (reader.TokenType != JsonToken.String)
        {
            throw new JsonSerializationException($"Unexpected token type: {reader.TokenType}. Expected String.");
        }

        var dateTimeString = reader.Value?.ToString();
        if (string.IsNullOrEmpty(dateTimeString))
        {
            return null;
        }

        if (DateTime.TryParse(dateTimeString, out DateTime parsedDateTime))
        {
            TimeZoneInfo sourceTimeZone;

            try
            {
                sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("America/Sao_Paulo"); // Para sistemas Linux/macOS
            }
            catch (TimeZoneNotFoundException)
            {
                sourceTimeZone = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time"); // Para Windows
            }
            
            var dateTimeOffset = new DateTimeOffset(parsedDateTime, sourceTimeZone.GetUtcOffset(parsedDateTime));

            return dateTimeOffset.ToUniversalTime().DateTime;
        }

        throw new JsonSerializationException($"Cannot convert string '{dateTimeString}' to DateTime.");
    }
}