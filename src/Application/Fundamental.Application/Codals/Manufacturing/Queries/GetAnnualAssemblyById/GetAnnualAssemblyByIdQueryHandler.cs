using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetAnnualAssemblyById;

public sealed class GetAnnualAssemblyByIdQueryHandler(
    IRepository repository
) : IRequestHandler<GetAnnualAssemblyByIdRequest, Response<GetAnnualAssemblyDetailItem>>
{
    public async Task<Response<GetAnnualAssemblyDetailItem>> Handle(
        GetAnnualAssemblyByIdRequest request,
        CancellationToken cancellationToken)
    {
        GetAnnualAssemblyDetailItem? result = await repository.FirstOrDefaultAsync(
            new AnnualAssemblyDetailItemSpec()
                .WhereId(request.Id),
            cancellationToken
        );

        if (result is null)
        {
            return GetAnnualAssemblyByIdErrorCodes.AnnualAssemblyNotFound;
        }

        return result;
    }
}
