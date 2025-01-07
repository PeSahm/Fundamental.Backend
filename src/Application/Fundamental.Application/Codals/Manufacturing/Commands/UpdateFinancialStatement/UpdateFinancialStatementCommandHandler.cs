using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateFinancialStatement;

public class UpdateFinancialStatementCommandHandler : IRequestHandler<UpdateFinancialStatementRequest, Response>
{
    private readonly IRepository<FinancialStatement> _statementRepository;
    private readonly IRepository<Symbol> _symbolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateFinancialStatementCommandHandler(
        IRepository<FinancialStatement> statementRepository,
        IRepository<Symbol> symbolRepository,
        IUnitOfWork unitOfWork
    )
    {
        _statementRepository = statementRepository;
        _symbolRepository = symbolRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(UpdateFinancialStatementRequest request, CancellationToken cancellationToken)
    {
        FinancialStatement? theStatement = await _statementRepository.FirstOrDefaultAsync(
            new FinancialStatementSpec().WhereId(request.Id),
            cancellationToken);

        if (theStatement is null)
        {
            return UpdateFinancialStatementErrorCodes.StatementNotFound;
        }

        Symbol? symbol = await _symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return UpdateFinancialStatementErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await _statementRepository.AnyAsync(
            new FinancialStatementSpec()
                .WhereTraceNo(request.TraceNo)
                .WhereIdNot(request.Id),
            cancellationToken);

        if (tracingNumberExists)
        {
            return UpdateFinancialStatementErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await _statementRepository.AnyAsync(
            new FinancialStatementSpec()
                .WhereSymbol(request.Isin)
                .WhereFiscalYear(request.FiscalYear)
                .WhereReportMonth(request.ReportMonth)
                .WhereIdNot(request.Id),
            cancellationToken);

        if (statementExists)
        {
            return UpdateFinancialStatementErrorCodes.DuplicateStatement;
        }

        theStatement.Update(
            symbol,
            request.TraceNo,
            request.Uri,
            request.FiscalYear,
            request.YearEndMonth,
            request.ReportMonth,
            request.OperatingIncome,
            request.GrossProfit,
            request.OperatingProfit,
            request.BankInterestIncome,
            request.InvestmentIncome,
            request.NetProfit,
            request.Expense,
            request.Asset,
            request.OwnersEquity,
            request.Receivables,
            DateTime.Now
        );

        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}