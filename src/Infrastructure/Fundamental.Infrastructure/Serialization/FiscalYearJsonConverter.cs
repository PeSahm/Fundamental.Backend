using System.Text.Json;
using System.Text.Json.Serialization;
using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Infrastructure.Serialization;

public class FiscalYearJsonConverter : JsonConverter<FiscalYear>
{
    public override FiscalYear Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            int year = reader.GetInt32();
            return new FiscalYear(year);
        }

        JsonElement yearObj = JsonSerializer.Deserialize<JsonElement>(ref reader);

        if (yearObj.TryGetProperty("Year", out JsonElement yearProperty))
        {
            int year = yearProperty.GetInt32();
            return new FiscalYear(year);
        }

        throw new JsonException("Invalid FiscalYear format");
    }

    public override void Write(Utf8JsonWriter writer, FiscalYear value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Year", value.Year);
        writer.WriteEndObject();
    }
}