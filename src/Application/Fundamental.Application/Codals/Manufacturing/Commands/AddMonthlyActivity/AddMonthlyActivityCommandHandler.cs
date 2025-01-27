using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddMonthlyActivity;

public sealed class AddMonthlyActivityCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<AddMonthlyActivityRequest, Response>
{
    public async Task<Response> Handle(AddMonthlyActivityRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await repository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return AddMonthlyActivityErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await repository.AnyAsync(
            new MonthlyActivitySpec().WhereTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddMonthlyActivityErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await repository.AnyAsync(
            new MonthlyActivitySpec()
                .WhereSymbol(request.Isin)
                .WhereFiscalYear(request.FiscalYear)
                .WhereReportMonth(request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddMonthlyActivityErrorCodes.DuplicateStatement;
        }

        MonthlyActivity statement = new MonthlyActivity(
            Guid.NewGuid(),
            symbol,
            request.TraceNo,
            request.Uri,
            request.FiscalYear,
            request.YearEndMonth,
            request.ReportMonth,
            new CodalMoney(request.SaleBeforeCurrentMonth),
            new CodalMoney(request.SaleCurrentMonth),
            new CodalMoney(request.SaleIncludeCurrentMonth),
            new CodalMoney(request.SaleLastYear),
            request.HasSubCompanySale,
            DateTime.Now
        );

        repository.Add(statement);
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}