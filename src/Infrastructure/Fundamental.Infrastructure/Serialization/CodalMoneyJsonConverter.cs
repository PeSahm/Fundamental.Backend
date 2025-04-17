using System.Text.Json;
using System.Text.Json.Serialization;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Infrastructure.Serialization;

public class CodalMoneyJsonConverter : JsonConverter<CodalMoney>
{
    public override CodalMoney Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        JsonElement moneyObj = JsonSerializer.Deserialize<JsonElement>(ref reader);
        
        if (moneyObj.TryGetProperty("Value", out JsonElement valueProperty) &&
            moneyObj.TryGetProperty("Currency", out JsonElement currencyProperty))
        {
            decimal value = valueProperty.GetDecimal();
            IsoCurrency currency = (IsoCurrency)currencyProperty.GetInt32();
            
            return new CodalMoney(value, currency);
        }

        throw new JsonException("Invalid CodalMoney format");
    }

    public override void Write(Utf8JsonWriter writer, CodalMoney value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteNumber("Value", value.Value);
        writer.WriteNumber("RealValue", value.RealValue);
        writer.WriteNumber("Currency", (int)value.Currency);
        writer.WriteEndObject();
    }
}