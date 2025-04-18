using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using Fundamental.Domain.Codals.Manufacturing.Events;
using Fundamental.Domain.Codals.ValueObjects;
using Fundamental.Domain.Common.ValueObjects;

namespace Fundamental.Infrastructure.Serialization;

public class MonthlyActivityUpdatedJsonConverter : JsonConverter<MonthlyActivityUpdated>
{
    public override MonthlyActivityUpdated Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        try
        {
            // Handle case where data is base64 encoded
            if (reader.TokenType == JsonTokenType.String)
            {
                string encodedJson = reader.GetString()!;

                if (encodedJson.StartsWith("data:IEvent;base64,"))
                {
                    string base64Data = encodedJson.Substring("data:IEvent;base64,".Length);
                    byte[] jsonBytes = Convert.FromBase64String(base64Data);
                    string jsonString = Encoding.UTF8.GetString(jsonBytes);
                    using JsonDocument base64Doc = JsonDocument.Parse(jsonString);
                    return DeserializeFromElement(base64Doc.RootElement);
                }

                // Handle regular JSON string
                using JsonDocument jsonDoc = JsonDocument.Parse(encodedJson);
                return DeserializeFromElement(jsonDoc.RootElement);
            }

            if (reader.TokenType != JsonTokenType.StartObject)
            {
                // If we get a JsonElement directly from CAP, try to deserialize it
                if (JsonSerializer.Deserialize<JsonElement>(ref reader) is JsonElement element)
                {
                    return DeserializeFromElement(element);
                }

                throw new JsonException("Expected either a JSON object, string, or JsonElement");
            }

            string? isinValue = null;
            StatementMonth? reportMonthValue = null;
            FiscalYear? fiscalYearValue = null;
            CodalMoney? saleCurrentMonthValue = null;
            CodalMoney? saleBeforeCurrentMonthValue = null;
            CodalMoney? saleLastYearValue = null;

            while (reader.Read())
            {
                if (reader.TokenType == JsonTokenType.EndObject)
                {
                    break;
                }

                if (reader.TokenType != JsonTokenType.PropertyName)
                {
                    throw new JsonException("Expected PropertyName token");
                }

                string propertyName = reader.GetString()!;
                reader.Read();

                switch (propertyName.ToLower())
                {
                    case "isin":
                        isinValue = reader.GetString();
                        break;

                    case "reportmonth":
                        int month = reader.TokenType == JsonTokenType.Number
                            ? reader.GetInt32()
                            : JsonSerializer.Deserialize<JsonElement>(ref reader).GetProperty("Month").GetInt32();
                        reportMonthValue = new StatementMonth(month);
                        break;

                    case "fiscalyear":
                        int year = reader.TokenType == JsonTokenType.Number
                            ? reader.GetInt32()
                            : JsonSerializer.Deserialize<JsonElement>(ref reader).GetProperty("Year").GetInt32();
                        fiscalYearValue = new FiscalYear(year);
                        break;

                    case "salecurrentmonth":
                        decimal currentMonthValue = reader.TokenType == JsonTokenType.Number
                            ? reader.GetDecimal()
                            : JsonSerializer.Deserialize<JsonElement>(ref reader).GetProperty("Value").GetDecimal();
                        saleCurrentMonthValue = new CodalMoney(currentMonthValue);
                        break;

                    case "salebeforecurrentmonth":
                        decimal beforeMonthValue = reader.TokenType == JsonTokenType.Number
                            ? reader.GetDecimal()
                            : JsonSerializer.Deserialize<JsonElement>(ref reader).GetProperty("Value").GetDecimal();
                        saleBeforeCurrentMonthValue = new CodalMoney(beforeMonthValue);
                        break;

                    case "salelastyear":
                        decimal lastYearValue = reader.TokenType == JsonTokenType.Number
                            ? reader.GetDecimal()
                            : JsonSerializer.Deserialize<JsonElement>(ref reader).GetProperty("Value").GetDecimal();
                        saleLastYearValue = new CodalMoney(lastYearValue);
                        break;
                }
            }

            ValidateRequiredProperties(isinValue,
                reportMonthValue,
                fiscalYearValue,
                saleCurrentMonthValue,
                saleBeforeCurrentMonthValue,
                saleLastYearValue);

            return new MonthlyActivityUpdated(
                isinValue!,
                reportMonthValue!,
                fiscalYearValue!,
                saleCurrentMonthValue!,
                saleBeforeCurrentMonthValue!,
                saleLastYearValue!);
        }
        catch (Exception ex) when (ex is not JsonException)
        {
            throw new JsonException($"Failed to deserialize MonthlyActivityUpdated: {ex.Message}", ex);
        }
    }

    public override void Write(Utf8JsonWriter writer, MonthlyActivityUpdated value, JsonSerializerOptions options)
    {
        writer.WriteStartObject();
        writer.WriteString("Isin", value.Isin);
        writer.WritePropertyName("ReportMonth");
        JsonSerializer.Serialize(writer, value.ReportMonth, options);
        writer.WritePropertyName("FiscalYear");
        JsonSerializer.Serialize(writer, value.FiscalYear, options);
        writer.WritePropertyName("SaleCurrentMonth");
        JsonSerializer.Serialize(writer, value.SaleCurrentMonth, options);
        writer.WritePropertyName("SaleBeforeCurrentMonth");
        JsonSerializer.Serialize(writer, value.SaleBeforeCurrentMonth, options);
        writer.WritePropertyName("SaleLastYear");
        JsonSerializer.Serialize(writer, value.SaleLastYear, options);
        writer.WriteEndObject();
    }

    private static MonthlyActivityUpdated DeserializeFromElement(JsonElement element)
    {
        string? isin = element.TryGetProperty("Isin", out JsonElement isinProp) ? isinProp.GetString() : null;

        StatementMonth? reportMonth = element.TryGetProperty("ReportMonth", out JsonElement reportMonthProp)
            ? new StatementMonth(reportMonthProp.GetProperty("Month").GetInt32())
            : null;

        FiscalYear? fiscalYear = element.TryGetProperty("FiscalYear", out JsonElement fiscalYearProp)
            ? new FiscalYear(fiscalYearProp.GetProperty("Year").GetInt32())
            : null;

        CodalMoney? saleCurrentMonth = element.TryGetProperty("SaleCurrentMonth", out JsonElement saleCurrentMonthProp)
            ? new CodalMoney(saleCurrentMonthProp.GetProperty("Value").GetDecimal())
            : null;

        CodalMoney? saleBeforeCurrentMonth = element.TryGetProperty("SaleBeforeCurrentMonth", out JsonElement saleBeforeCurrentMonthProp)
            ? new CodalMoney(saleBeforeCurrentMonthProp.GetProperty("Value").GetDecimal())
            : null;

        CodalMoney? saleLastYear = element.TryGetProperty("SaleLastYear", out JsonElement saleLastYearProp)
            ? new CodalMoney(saleLastYearProp.GetProperty("Value").GetDecimal())
            : null;

        ValidateRequiredProperties(isin, reportMonth, fiscalYear, saleCurrentMonth, saleBeforeCurrentMonth, saleLastYear);

        return new MonthlyActivityUpdated(
            isin!,
            reportMonth!,
            fiscalYear!,
            saleCurrentMonth!,
            saleBeforeCurrentMonth!,
            saleLastYear!);
    }

    private static void ValidateRequiredProperties(
        string? isin,
        StatementMonth? reportMonth,
        FiscalYear? fiscalYear,
        CodalMoney? saleCurrentMonth,
        CodalMoney? saleBeforeCurrentMonth,
        CodalMoney? saleLastYear
    )
    {
        if (isin == null)
        {
            throw new JsonException("Missing required property: Isin");
        }

        if (reportMonth == null)
        {
            throw new JsonException("Missing required property: ReportMonth");
        }

        if (fiscalYear == null)
        {
            throw new JsonException("Missing required property: FiscalYear");
        }

        if (saleCurrentMonth == null)
        {
            throw new JsonException("Missing required property: SaleCurrentMonth");
        }

        if (saleBeforeCurrentMonth == null)
        {
            throw new JsonException("Missing required property: SaleBeforeCurrentMonth");
        }

        if (saleLastYear == null)
        {
            throw new JsonException("Missing required property: SaleLastYear");
        }
    }
}