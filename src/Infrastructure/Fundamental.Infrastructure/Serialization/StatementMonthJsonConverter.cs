using System.Text.Json;
using System.Text.Json.Serialization;
using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Infrastructure.Serialization;

public class StatementMonthJsonConverter : JsonConverter<StatementMonth>
{
    public override StatementMonth Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType == JsonTokenType.Number)
        {
            int month = reader.GetInt32();
            return new StatementMonth(month);
        }

        JsonElement monthObj = JsonSerializer.Deserialize<JsonElement>(ref reader);

        if (monthObj.TryGetProperty("Month", out JsonElement monthProperty))
        {
            int month = monthProperty.GetInt32();
            return new StatementMonth(month);
        }

        throw new JsonException("Invalid StatementMonth format");
    }

    public override void Write(Utf8JsonWriter writer, StatementMonth value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Month", value.Month);
        writer.WriteEndObject();
    }
}