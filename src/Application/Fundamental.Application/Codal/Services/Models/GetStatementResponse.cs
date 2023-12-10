using Fundamental.Application.Codal.Services.Enums;

namespace Fundamental.Application.Codal.Services.Models;

public class GetStatementResponse
{
    public DateTime PublishDate { get; set; }

    public ReportingType ReportingType { get; set; }

    public string HtmlUrl { get; set; }

    public string Isin { get; set; }

    public ulong TracingNo { get; set; }
}