using Fundamental.Application.Codal.Enums;
using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
using Fundamental.Domain.Statements.Enums;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codal.Commands.UpdateMonthlyActivityData;

public sealed class UpdateMonthlyActivityDataCommandHandler : IRequestHandler<UpdateMonthlyActivityDataRequest, Response>
{
    private readonly ICodalService _codalService;

    public UpdateMonthlyActivityDataCommandHandler(ICodalService codalService)
    {
        _codalService = codalService;
    }

    public async Task<Response> Handle(UpdateMonthlyActivityDataRequest request, CancellationToken cancellationToken)
    {
        List<GetStatementResponse> monthlyActivities =
            await _codalService.GetStatements(
                DateTime.Now.AddDays(request.Days),
                ReportingType.Production,
                LetterType.MonthlyActivity,
                cancellationToken);

        foreach (GetStatementResponse monthlyActivity in monthlyActivities)
        {
            try
            {
                await _codalService.ProcessCodal(monthlyActivity, LetterPart.NotSpecified, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return Response.Successful();
    }
}