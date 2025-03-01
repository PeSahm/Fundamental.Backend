using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using MediatR;
using Polly;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

public sealed class UpdateFinancialStatementsDataCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork,
    IFinancialStatementBuilder financialStatementBuilder,
    IAsyncPolicy asyncPolicy
)
    : IRequestHandler<UpdateFinancialStatementsDataRequest>
{
    public async Task Handle(UpdateFinancialStatementsDataRequest request, CancellationToken cancellationToken)
    {
        List<SimpleBalanceSheet> balanceSheetList =
            await repository.ListAsync(new BalanceSheetSpec().ToSimpleBalanceSheetSpec(), cancellationToken);

        foreach (SimpleBalanceSheet balanceSheet in balanceSheetList)
        {
            FinancialStatement fs = financialStatementBuilder.SetId(Guid.NewGuid())
                .SetSymbol(balanceSheet.Symbol)
                .SetCurrency(IsoCurrency.IRR)
                .SetTraceNo(balanceSheet.TraceNo)
                .SetFiscalYear(balanceSheet.FiscalYear)
                .SetYearEndMonth(balanceSheet.YearEndMonth)
                .SetCreatedAt(DateTime.UtcNow)
                .Build();
            repository.Add(fs);
            await asyncPolicy.ExecuteAsync(async () =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            });
        }
    }
}