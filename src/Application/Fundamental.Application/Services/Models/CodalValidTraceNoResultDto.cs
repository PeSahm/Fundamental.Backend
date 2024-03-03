using Fundamental.Domain.Codals.ValueObjects;

namespace Fundamental.Application.Services.Models;

public sealed class CodalValidTraceNoResultDto
{
    public ulong TraceNo { get; init; }

    public StatementMonth Month { get; init; }

    public FiscalYear Year { get; set; }

    public string Isin { get; set; }
}