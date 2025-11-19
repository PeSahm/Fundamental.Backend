using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;

public sealed class GetExtraAnnualAssemblyByIdQueryHandler(
    IRepository repository
) : IRequestHandler<GetExtraAnnualAssemblyByIdRequest, Response<GetExtraAnnualAssemblyDetailItem>>
{
    public async Task<Response<GetExtraAnnualAssemblyDetailItem>> Handle(
        GetExtraAnnualAssemblyByIdRequest request,
        CancellationToken cancellationToken)
    {
        GetExtraAnnualAssemblyDetailItem? result = await repository.FirstOrDefaultAsync(
            new ExtraAnnualAssemblyDetailItemSpec()
                .WhereId(request.Id),
            cancellationToken
        );

        if (result is null)
        {
            return GetExtraAnnualAssemblyByIdErrorCodes.ExtraAnnualAssemblyNotFound;
        }

        return result;
    }
}
