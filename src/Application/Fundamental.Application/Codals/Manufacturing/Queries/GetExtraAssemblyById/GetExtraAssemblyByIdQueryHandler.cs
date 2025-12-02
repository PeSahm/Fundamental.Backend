using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAssemblyById;

public sealed class GetExtraAssemblyByIdQueryHandler(
    IRepository repository
) : IRequestHandler<GetExtraAssemblyByIdRequest, Response<GetExtraAssemblyDetailItem>>
{
    public async Task<Response<GetExtraAssemblyDetailItem>> Handle(
        GetExtraAssemblyByIdRequest request,
        CancellationToken cancellationToken)
    {
        GetExtraAssemblyDetailItem? result = await repository.FirstOrDefaultAsync(
            new ExtraAssemblyDetailItemSpec()
                .WhereId(request.Id),
            cancellationToken
        );

        if (result is null)
        {
            return GetExtraAssemblyByIdErrorCodes.ExtraAssemblyNotFound;
        }

        return result;
    }
}
