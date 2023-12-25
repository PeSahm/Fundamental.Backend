using Fundamental.Application.CodalSorts.Queries.GetBalanceSheetSort;
using Fundamental.Application.CodalSorts.Specifications;
using Fundamental.Application.Statements.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Statements.Entities;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Statements.Commands.AddBalanceSheet;

public sealed class AddBalanceSheetCommandHandler(
    IRepository<BalanceSheet> balanceSheetRepository,
    IRepository<BalanceSheetSort> balanceSheetSortRepository,
    IRepository<Symbol> symbolRepository,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<AddBalanceSheetRequest, Response>
{
    public async Task<Response> Handle(AddBalanceSheetRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await symbolRepository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return AddBalanceSheetErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await balanceSheetRepository.AnyAsync(
            BalanceSheetSpec.WithTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddBalanceSheetErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await balanceSheetRepository.AnyAsync(
            BalanceSheetSpec.Where(request.TraceNo, request.Isin, request.FiscalYear, request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddBalanceSheetErrorCodes.DuplicateStatement;
        }

        List<GetBalanceSheetSortResultDto> codaRows =
            await balanceSheetSortRepository.ListAsync(BalanceSheetSortSpec.GetValidSpecifications(), cancellationToken);

        foreach (GetBalanceSheetSortResultDto sheetItem in codaRows)
        {
            if (!request.Items.Exists(x => x.CodalRow == sheetItem.CodalRow && x.CodalCategory == sheetItem.Category))
            {
                return Error.FromErrorCode(
                    AddBalanceSheetErrorCodes.SomeCodalRowsAreInvalid,
                    new Dictionary<string, string>()
                    {
                        { "CodalRow", sheetItem.CodalRow.ToString() },
                    });
            }
        }

        foreach (AddBalanceSheetItem item in request.Items)
        {
            GetBalanceSheetSortResultDto balanceSheetRow =
                codaRows.First(x => x.CodalRow == item.CodalRow && x.Category == item.CodalCategory);
            BalanceSheet balanceSheet = new BalanceSheet(
                Guid.NewGuid(),
                symbol,
                request.TraceNo,
                request.Uri,
                request.FiscalYear,
                yearEndMonth: request.YearEndMonth,
                reportMonth: request.ReportMonth,
                row: balanceSheetRow.Order,
                balanceSheetRow.CodalRow,
                balanceSheetRow.Category,
                balanceSheetRow.Description,
                item.Value,
                request.IsAudited,
                DateTime.Now
            );
            balanceSheetRepository.Add(balanceSheet);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}