using System.Text;
using DNTPersianUtils.Core;
using FluentValidation;
using Fundamental.Application.Common.Extensions;
using Fundamental.Application.Statements.Repositories;
using Fundamental.Domain.Codals.Entities;
using Fundamental.Domain.Codals.Enums;
using Fundamental.Domain.Common.Dto;
using Fundamental.ErrorHandling;
using Fundamental.ErrorHandling.Attributes;
using Fundamental.ErrorHandling.Enums;
using MediatR;

namespace Fundamental.Application.Statements.Queries.GetBalanceSheets;

[HandlerCode(HandlerCode.GetBalanceSheet)]
public sealed record GetBalanceSheetRequest(List<string> IsinList, ulong? TraceNo, ushort? FiscalYear, ushort? ReportMonth) :
    PagingRequest, IRequest<Response<Paginated<GetBalanceSheetResultDto>>>;

public sealed class GetBalanceSheetResultDto
{
    public Guid Id { get; set; }
    public string Isin { get; init; }
    public string Symbol { get; init; }
    public ulong TraceNo { get; init; }
    public string Uri { get; init; }
    public ushort FiscalYear { get; init; }
    public ushort YearEndMonth { get; init; }
    public ushort ReportMonth { get; init; }
    public bool IsAudited { get; init; }
    public string IsAuditedDescription => IsAudited ? "حسابرسی شده" : "حسابرسی نشده";

    public string Title => new StringBuilder("گزارش صورت وضعیت مالی نماد ")
        .Append(Symbol)
        .Append(" دوره ")
        .Append(ReportMonth)
        .Append(" ماهه ")
        .Append(" منتهی به ")
        .Append($"{FiscalYear}/{YearEndMonth}/{GetLastDayOfFiscalYear()}")
        .Append(' ')
        .Append(IsAuditedDescription)
        .ToString();

    public List<GetBalanceSheetResultItem> Items { get; init; } = new();

    private int GetLastDayOfFiscalYear()
    {
        int lastDay = $"{FiscalYear}/{YearEndMonth}/1".ToGregorianDateOnly().GetPersianMonthStartAndEndDates()!.LastDayNumber;
        return lastDay;
    }
}

public sealed class GetBalanceSheetResultItem
{
    public ushort Order { get; init; }
    public ushort CodalRow { get; init; }

    public string Description => BalanceSheetSort.GetDescription(CodalRow, Category);
    public required BalanceSheetCategory Category { get; init; }

    public string? CategoryDescription => Category.GetDescription();
    public decimal Value { get; init; }
}

public sealed class GetBalanceSheetRequestValidator : AbstractValidator<GetBalanceSheetRequest>
{
    public GetBalanceSheetRequestValidator()
    {
        RuleFor(x => x.PageSize).GreaterThan(0)
            .WithMessage("PageSize must be greater than 0");

        RuleFor(x => x.PageNumber).GreaterThan(0)
            .WithMessage("PageNumber must be greater than 0");
    }
}

public sealed class GetBalanceSheetQueryHandler(IBalanceSheetReadRepository balanceSheetReadRepository)
    : IRequestHandler<GetBalanceSheetRequest, Response<Paginated<GetBalanceSheetResultDto>>>
{
    public async Task<Response<Paginated<GetBalanceSheetResultDto>>> Handle(
        GetBalanceSheetRequest request,
        CancellationToken cancellationToken
    )
    {
        return await balanceSheetReadRepository.GetBalanceSheet(request, cancellationToken);
    }
}