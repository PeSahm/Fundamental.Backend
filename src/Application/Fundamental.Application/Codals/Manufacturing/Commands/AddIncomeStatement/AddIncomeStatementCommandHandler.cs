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
            IncomeStatementSpec.WithTraceNo(request.TraceNo),
            cancellationToken);

        if (tracingNumberExists)
        {
            return AddIncomeStatementErrorCodes.DuplicateTraceNo;
        }

        bool statementExists = await repository.AnyAsync(
            IncomeStatementSpec.Where(request.TraceNo, request.Isin, request.FiscalYear, request.ReportMonth),
            cancellationToken);

        if (statementExists)
        {
            return AddIncomeStatementErrorCodes.DuplicateStatement;
        }

        List<GetIncomeStatementSortResultDto> codaRows =
            await repository.ListAsync(IncomeStatementSortSpec.GetValidSpecifications(), cancellationToken);

        foreach (GetIncomeStatementSortResultDto sheetItem in codaRows)
        {
            if (!request.Items.Exists(x => x.CodalRow == sheetItem.CodalRow && x.Row == sheetItem.Order))
            {
                return Error.FromErrorCode(
                    AddIncomeStatementErrorCodes.SomeCodalRowsAreInvalid,
                    new Dictionary<string, string>()
                    {
                        { "CodalRow", sheetItem.CodalRow.ToString() },
                    });
            }
        }

        foreach (AddIncomeStatementItem item in request.Items)
        {
            GetIncomeStatementSortResultDto incomeStatementRow =
                codaRows.First(x => x.CodalRow == item.CodalRow && x.Order == item.Row);
            IncomeStatement incomeStatement = new IncomeStatement(
                Guid.NewGuid(),
                symbol,
                request.TraceNo,
                request.Uri,
                fiscalYear: request.FiscalYear,
                yearEndMonth: request.YearEndMonth,
                reportMonth: request.ReportMonth,
                row: item.Row,
                item.CodalRow,
                incomeStatementRow.Description,
                item.Value,
                request.IsAudited,
                DateTime.UtcNow
            );
            repository.Add(incomeStatement);
        }

        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}