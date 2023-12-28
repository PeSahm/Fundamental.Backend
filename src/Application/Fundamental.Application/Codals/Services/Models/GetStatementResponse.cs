using Fundamental.Domain.Common.Enums;

namespace Fundamental.Application.Codals.Services.Models;

public class GetStatementResponse
{
    public DateTime PublishDate { get; set; }
    public ReportingType ReportingType { get; set; }
    public LetterType Type { get; set; }
    public string HtmlUrl { get; set; }
    public string Isin { get; set; }
    public ulong TracingNo { get; set; }
}