using MediatR;
using FluentValidation;
namespace Internship_4_OOP.Application.Common.Behaviours;

public class RequestValidationBehavior<TRequest, TResponse>(IReadOnlyList<IValidator<TRequest>> validators)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly IReadOnlyList<IValidator<TRequest>> _validators = validators;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        
        cancellationToken.ThrowIfCancellationRequested();
        
        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(validator => validator.ValidateAsync(context)));

        var failures = results.Where(result => !result.IsValid).SelectMany(result => result.Errors).ToList();
        cancellationToken.ThrowIfCancellationRequested();
        
        if (failures.Count != 0)
            throw new ValidationException("Validacija unosa neuspješna",failures);
        return await next(cancellationToken);
    }
}
