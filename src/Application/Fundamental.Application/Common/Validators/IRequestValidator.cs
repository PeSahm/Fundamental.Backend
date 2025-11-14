using FluentValidation.Results;

namespace Fundamental.Application.Common.Validators;

public interface IRequestValidator<in TRequest>
{
    Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken);
}