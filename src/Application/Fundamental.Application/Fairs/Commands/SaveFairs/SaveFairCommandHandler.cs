using Fundamental.Domain.ExAreas.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Fairs.Commands.SaveFairs;

public sealed class SaveFairCommandHandler(IRepository<Fair> repository, IUnitOfWork unitOfWork)
    : IRequestHandler<SaveFairRequest, Response>
{
    public async Task<Response> Handle(SaveFairRequest request, CancellationToken cancellationToken)
    {
        repository.Add(new Fair(request.Json));
        await unitOfWork.SaveChangesAsync(cancellationToken);
        return Response.Successful();
    }
}