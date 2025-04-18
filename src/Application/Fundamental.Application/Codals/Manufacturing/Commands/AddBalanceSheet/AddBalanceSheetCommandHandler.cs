using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddBalanceSheet;

public sealed class AddBalanceSheetCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<AddBalanceSheetRequest, Response>
{
    public async Task<Response> Handle(AddBalanceSheetRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await repository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return AddBalanceSheetErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await repository.AnyAsync(
            BalanceSheetSpec.WithTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddBalanceSheetErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await repository.AnyAsync(
            BalanceSheetSpec.Where(request.TraceNo, request.Isin, request.FiscalYear, request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddBalanceSheetErrorCodes.DuplicateStatement;
        }

        List<GetBalanceSheetSortResultDto> codaRows =
            await repository.ListAsync(BalanceSheetSortSpec.GetValidSpecifications(), cancellationToken);

        foreach (GetBalanceSheetSortResultDto sheetItem in codaRows)
        {
            if (!request.Items.Exists(x => x.CodalRow == sheetItem.CodalRow && x.CodalCategory == sheetItem.Category))
            {
                return Error.FromErrorCode(
                    AddBalanceSheetErrorCodes.SomeCodalRowsAreInvalid,
                    new Dictionary<string, string>
                    {
                        { "CodalRow", sheetItem.CodalRow.ToString() }
                    });
            }
        }

        foreach (AddBalanceSheetItem item in request.Items)
        {
            GetBalanceSheetSortResultDto balanceSheetRow =
                codaRows.First(x => x.CodalRow == item.CodalRow && x.Category == item.CodalCategory);
            BalanceSheet balanceSheet = new(
                Guid.NewGuid(),
                symbol,
                request.TraceNo,
                request.Uri,
                request.FiscalYear,
                request.YearEndMonth,
                request.ReportMonth,
                balanceSheetRow.Order,
                balanceSheetRow.CodalRow,
                balanceSheetRow.Category,
                balanceSheetRow.Description,
                item.Value,
                request.IsAudited,
                DateTime.Now
            );
            repository.Add(balanceSheet);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}