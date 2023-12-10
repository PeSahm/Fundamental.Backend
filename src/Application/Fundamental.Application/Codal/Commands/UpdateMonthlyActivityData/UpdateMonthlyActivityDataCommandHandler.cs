using Fundamental.Application.Codal.Services;
using Fundamental.Application.Codal.Services.Enums;
using Fundamental.Application.Codal.Services.Models;
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
            await _codalService.GetMonthlyActivities(new DateTime(2022, 10, 24), ReportingType.Production, cancellationToken);

        foreach (GetStatementResponse monthlyActivity in monthlyActivities)
        {
            try
            {
                await _codalService.UpsertMonthlyActivities(monthlyActivity, cancellationToken);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        return Response.Successful();
    }
}