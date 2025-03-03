using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Builders.FinancialStatements;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Common.Enums;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using MediatR;
using Polly;
using Polly.Registry;

namespace Fundamental.Application.Codals.Manufacturing.Jobs.UpdateFinancialStatementsData;

public sealed class UpdateFinancialStatementsDataCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork,
    IFinancialStatementBuilder financialStatementBuilder,
    ResiliencePipelineProvider<string> pipelineProvider
)
    : IRequestHandler<UpdateFinancialStatementsDataRequest>
{
    public async Task Handle(UpdateFinancialStatementsDataRequest request, CancellationToken cancellationToken)
    {
        List<SimpleBalanceSheet> balanceSheetList =
            await repository.ListAsync(
                new BalanceSheetSpec()
                    .WhereNoFinancialStatement()
                    .ToSimpleBalanceSheetSpec(),
                cancellationToken);
        ResiliencePipeline pipeline = pipelineProvider.GetPipeline("DbUpdateConcurrencyException");

        foreach (SimpleBalanceSheet balanceSheet in balanceSheetList)
        {
            Symbol? symbol = await repository.FirstOrDefaultAsync(new SymbolSpec().WhereIsin(balanceSheet.Isin), cancellationToken);

            if (symbol is null)
            {
                continue;
            }

            FinancialStatement fs = financialStatementBuilder.SetId(Guid.NewGuid())
                .SetSymbol(symbol)
                .SetCurrency(IsoCurrency.IRR)
                .SetTraceNo(balanceSheet.TraceNo)
                .SetFiscalYear(balanceSheet.FiscalYear)
                .SetYearEndMonth(balanceSheet.YearEndMonth)
                .SetCreatedAt(DateTime.Now)
                .Build();
            repository.Add(fs);
        }

        await pipeline.ExecuteAsync(
            async _ =>
            {
                await unitOfWork.SaveChangesAsync(cancellationToken);
            },
            cancellationToken);
    }
}