using Fundamental.Application.Codals.Manufacturing.Queries.GetIncomeStatementSort;
using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.Domain.Symbols.Entities;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.AddIncomeStatement;

public sealed class AddIncomeStatementCommandHandler(
    IRepository repository,
    IUnitOfWork unitOfWork
)
    : IRequestHandler<AddIncomeStatementRequest, Response>
{
    public async Task<Response> Handle(AddIncomeStatementRequest request, CancellationToken cancellationToken)
    {
        Symbol? symbol = await repository.FirstOrDefaultAsync(
            new SymbolSpec().WhereIsin(request.Isin),
            cancellationToken);

        if (symbol is null)
        {
            return AddIncomeStatementErrorCodes.SymbolNotFound;
        }

        bool tracingNumberExists = await repository.AnyAsync(
            new IncomeStatementSpec().WithTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddIncomeStatementErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await repository.AnyAsync(
            IncomeStatementSpec.Where(
                request.TraceNo,
                request.Isin,
                request.YearEndMonth,
                request.FiscalYear,
                request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddIncomeStatementErrorCodes.DuplicateStatement;
        }

        List<GetIncomeStatementSortResultDto> codaRows =
            await repository.ListAsync(IncomeStatementSortSpec.GetValidSpecifications(), cancellationToken);

        if (codaRows.Any(sheetItem => !request.Items.Exists(x => x.CodalRow == sheetItem.CodalRow && x.Row == sheetItem.Order)))
        {
            GetIncomeStatementSortResultDto invalidItem = codaRows.First(sheetItem => !request.Items.Exists(x => x.CodalRow == sheetItem.CodalRow && x.Row == sheetItem.Order));
            return Error.FromErrorCode(
                AddIncomeStatementErrorCodes.SomeCodalRowsAreInvalid,
                new Dictionary<string, string>
                {
                    { "CodalRow", invalidItem.CodalRow.ToString() }
                });
        }

        IncomeStatement incomeStatement = new(
            Guid.NewGuid(),
            symbol,
            request.TraceNo,
            request.Uri,
            request.FiscalYear,
            request.YearEndMonth,
            request.ReportMonth,
            request.IsAudited,
            DateTime.UtcNow,
            request.PublishDate
        );

        foreach (AddIncomeStatementItem item in request.Items)
        {
            GetIncomeStatementSortResultDto incomeStatementRow =
                codaRows.First(x => x.CodalRow == item.CodalRow && x.Order == item.Row);
            IncomeStatementDetail detail = new(
                Guid.NewGuid(),
                incomeStatement,
                item.Row,
                item.CodalRow,
                incomeStatementRow.Description,
                item.Value,
                DateTime.UtcNow
            );
            incomeStatement.Details.Add(detail);
        }

        repository.Add(incomeStatement);

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}