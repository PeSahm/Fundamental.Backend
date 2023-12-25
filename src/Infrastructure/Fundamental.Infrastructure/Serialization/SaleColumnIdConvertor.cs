using System.Text.Json;
using System.Text.Json.Serialization;
using Fundamental.Application.Codals.Dto.MonthlyActivities.V4.Enums;

namespace Fundamental.Infrastructure.Serialization;

public class SaleColumnIdConvertor : JsonConverter<SaleColumnId>
{
    public override SaleColumnId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return (SaleColumnId)int.Parse(reader.GetString()!);
    }

    public override void Write(Utf8JsonWriter writer, SaleColumnId value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(((int)value).ToString());
    }
}