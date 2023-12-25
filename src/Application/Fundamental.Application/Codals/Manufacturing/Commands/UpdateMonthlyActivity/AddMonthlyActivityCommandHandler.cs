using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateMonthlyActivity;

public sealed class AddMonthlyActivityCommandHandler : IRequestHandler<UpdateMonthlyActivityRequest, Response>
{
    private readonly IRepository<MonthlyActivity> _monthlyActivityRepository;
    private readonly IRepository<Symbol> _symbolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddMonthlyActivityCommandHandler(
        IRepository<MonthlyActivity> monthlyActivityRepository,
        IRepository<Symbol> symbolRepository,
        IUnitOfWork unitOfWork
    )
    {
        _monthlyActivityRepository = monthlyActivityRepository;
        _symbolRepository = symbolRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(UpdateMonthlyActivityRequest request, CancellationToken cancellationToken)
    {
        MonthlyActivity? theStatement = await _monthlyActivityRepository.FirstOrDefaultAsync(
            new MonthlyActivitySpec()
                .WhereId(request.Id),
            cancellationToken);

        if (theStatement is null)
        {
            return UpdateMonthlyActivityErrorCodes.MonthlyActivityNotFound;
        }

        Symbol? symbol = await _symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return UpdateMonthlyActivityErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await _monthlyActivityRepository.AnyAsync(
            new MonthlyActivitySpec()
                .WhereTraceNo(request.TraceNo)
                .WhereIdNot(request.Id),
            cancellationToken);

        if (tracingNumberExists)
        {
            return UpdateMonthlyActivityErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await _monthlyActivityRepository.AnyAsync(
            new MonthlyActivitySpec()
                .WhereSymbol(request.Isin)
                .WhereFiscalYear(request.FiscalYear)
                .WhereReportMonth(request.ReportMonth)
                .WhereIdNot(request.Id),
            cancellationToken);

        if (statementExists)
        {
            return UpdateMonthlyActivityErrorCodes.DuplicateStatement;
        }

        theStatement.Update(
            symbol,
            request.TraceNo,
            request.Uri,
            request.FiscalYear,
            request.YearEndMonth,
            request.ReportMonth,
            request.SaleBeforeCurrentMonth,
            request.SaleCurrentMonth,
            request.SaleIncludeCurrentMonth,
            request.SaleLastYear,
            request.HasSubCompanySale,
            DateTime.Now
        );
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}