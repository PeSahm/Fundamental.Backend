using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Queries.GetExtraAnnualAssemblyById;

public sealed class GetExtraAnnualAssemblyByIdQueryHandler(
    IRepository repository
) : IRequestHandler<GetExtraAnnualAssemblyByIdRequest, Response<GetExtraAnnualAssemblyDetailItem>>
{
    /// <summary>
    /// Handles a query to retrieve an ExtraAnnualAssembly detail by its identifier.
    /// </summary>
    /// <param name="request">The query containing the identifier of the ExtraAnnualAssembly to retrieve.</param>
    /// <param name="cancellationToken">Token to observe while awaiting the operation.</param>
    /// <returns>
    /// A <see cref="Response{GetExtraAnnualAssemblyDetailItem}"/> containing the requested detail item, or a response with the <c>ExtraAnnualAssemblyNotFound</c> error code if no item exists for the given identifier.
    /// </returns>
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