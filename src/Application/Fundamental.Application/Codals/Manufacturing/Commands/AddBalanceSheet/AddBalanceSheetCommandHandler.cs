using Fundamental.Application.Codals.Manufacturing.Queries.GetBalanceSheetSort;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Codals.ValueObjects;
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

        // Check if BalanceSheet already exists
        BalanceSheet? existingBalanceSheet = await repository.FirstOrDefaultAsync(
            BalanceSheetSpec.Where(
                request.TraceNo,
                request.Isin,
                request.FiscalYear,
                request.YearEndMonth,
                request.ReportMonth),
            cancellationToken);

        if (existingBalanceSheet != null)
        {
            return AddBalanceSheetErrorCodes.DuplicateStatement;
        }

        List<GetBalanceSheetSortResultDto> codaRows =
            await repository.ListAsync(BalanceSheetSortSpec.GetValidSpecifications(), cancellationToken);

        GetBalanceSheetSortResultDto? failingItem = codaRows.FirstOrDefault(sheetItem =>
            !request.Items.Any(x => x.CodalRow == sheetItem.CodalRow && x.CodalCategory == sheetItem.Category));

        if (failingItem != null)
        {
            return Error.FromErrorCode(
                AddBalanceSheetErrorCodes.SomeCodalRowsAreInvalid,
                new Dictionary<string, string>
                {
                    { "CodalRow", failingItem.CodalRow.ToString() }
                });
        }

        // Create BalanceSheet header
        BalanceSheet balanceSheet = new BalanceSheet(
            Guid.NewGuid(),
            symbol,
            request.TraceNo,
            request.Uri,
            new FiscalYear(request.FiscalYear),
            new StatementMonth(request.YearEndMonth),
            new StatementMonth(request.ReportMonth),
            request.IsAudited,
            DateTime.Now,
            request.PublishDate
        );

        repository.Add(balanceSheet);

        foreach (AddBalanceSheetItem item in request.Items)
        {
            GetBalanceSheetSortResultDto balanceSheetRow =
                codaRows.First(x => x.CodalRow == item.CodalRow && x.Category == item.CodalCategory);

            BalanceSheetDetail detail = new BalanceSheetDetail(
                Guid.NewGuid(),
                balanceSheet,
                balanceSheetRow.Order,
                balanceSheetRow.CodalRow,
                balanceSheetRow.Category,
                balanceSheetRow.Description,
                item.Value,
                DateTime.Now
            );

            repository.Add(detail);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}