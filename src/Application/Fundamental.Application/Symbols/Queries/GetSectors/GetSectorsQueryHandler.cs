using Fundamental.Application.Symbols.Specifications;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Symbols.Queries.GetSectors;

public sealed class GetSectorsQueryHandler(IRepository repository)
    : IRequestHandler<GetSectorsRequest, Response<List<GetSectorsResultDto>>>
{
    public async Task<Response<List<GetSectorsResultDto>>> Handle(GetSectorsRequest request, CancellationToken cancellationToken)
    {
        SectorSpec spec = new SectorSpec().Filter(request.Filter);
        List<GetSectorsResultDto> sectors = await repository.ListAsync(spec, cancellationToken);
        return sectors;
    }
}