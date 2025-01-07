using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.ValueObjects;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddFinancialStatement;

public class AddFinancialStatementCommandHandler : IRequestHandler<AddFinancialStatementRequest, Response>
{
    private readonly IRepository<FinancialStatement> _statementRepository;
    private readonly IRepository<Symbol> _symbolRepository;
    private readonly IUnitOfWork _unitOfWork;

    public AddFinancialStatementCommandHandler(
        IRepository<FinancialStatement> statementRepository,
        IRepository<Symbol> symbolRepository,
        IUnitOfWork unitOfWork
    )
    {
        _statementRepository = statementRepository;
        _symbolRepository = symbolRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Response> Handle(AddFinancialStatementRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await _symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return AddFinancialStatementErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await _statementRepository.AnyAsync(
            new FinancialStatementSpec().WhereTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddFinancialStatementErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await _statementRepository.AnyAsync(
            new FinancialStatementSpec()
                .WhereSymbol(request.Isin)
                .WhereFiscalYear(request.FiscalYear)
                .WhereReportMonth(request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddFinancialStatementErrorCodes.DuplicateStatement;
        }

        FinancialStatement statement = new FinancialStatement(
            Guid.NewGuid(),
            symbol,
            request.TraceNo,
            request.Uri,
            request.FiscalYear,
            request.YearEndMonth,
            request.ReportMonth,
            new CodalMoney(request.OperatingIncome),
            new SignedCodalMoney(request.GrossProfit),
            new SignedCodalMoney(request.OperatingProfit),
            new CodalMoney(request.BankInterestIncome),
            new SignedCodalMoney(request.InvestmentIncome),
            new CodalMoney(request.NetProfit),
            new CodalMoney(request.Expense),
            new CodalMoney(request.Asset),
            new CodalMoney(request.OwnersEquity),
            new CodalMoney(request.Receivables),
            DateTime.Now
        );

        _statementRepository.Add(statement);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}