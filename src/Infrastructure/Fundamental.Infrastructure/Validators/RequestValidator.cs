using FluentValidation;
using FluentValidation.Results;
using Fundamental.Application.Common.Validators;

namespace Fundamental.Infrastructure.Validators;

public class RequestValidator<TRequest> : IRequestValidator<TRequest>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public RequestValidator(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<List<ValidationFailure>> ValidateAsync(TRequest request, CancellationToken cancellationToken)
    {
        ValidationContext<TRequest> context = new(request);

        ValidationResult[] validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        return validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors)
            .ToList();
    }
}