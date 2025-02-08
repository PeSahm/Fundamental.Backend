using Fundamental.Application.Codals.Manufacturing.Specifications;
using Fundamental.Domain.Codals.Manufacturing.Entities;
using Fundamental.Domain.Repositories.Base;
using Fundamental.ErrorHandling;
using MediatR;

namespace Fundamental.Application.Codals.Manufacturing.Commands.UpdateNoneOperationalIncomeTags;

public sealed class UpdateNoneOperationalIncomeTagsCommandHandler(IRepository repository, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateNoneOperationalIncomeTagsRequest, Response>
{
    public async Task<Response> Handle(UpdateNoneOperationalIncomeTagsRequest request, CancellationToken cancellationToken)
    {
        NonOperationIncomeAndExpense? entity =
            await repository.FirstOrDefaultAsync(new NonOperationIncomeAndExpenseSpec().GetById(request.Id), cancellationToken);

        if (entity is null)
        {
            return UpdateNoneOperationalIncomeTagsErrorCodes.EntityNotFound;
        }

        entity.UpdateTags(request.Tags.ToArray());
        await unitOfWork.SaveChangesAsync(cancellationToken);

        return Response.Successful();
    }
}