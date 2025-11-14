using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services.Models.CodelServiceModels;

public class GetStatementResponse
{
    public DateTime PublishDateMiladi { get; set; }
    public ReportingType ReportingType { get; set; }
    public LetterType Type { get; set; }
    public string HtmlUrl { get; set; }
    public long PublisherId { get; set; }
    public ulong TracingNo { get; set; }
    public string? Isin { get; set; }
}