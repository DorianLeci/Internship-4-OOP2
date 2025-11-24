using MediatR;
using FluentValidation;
namespace Internship_4_OOP.Application.Common.Behaviours;

public class RequestValidationBehavior<TRequest,TResponse> :IPipelineBehavior<TRequest,TResponse> where TRequest:notnull
{
    private readonly IReadOnlyList<IValidator<TRequest>> _validators;

    public RequestValidationBehavior(IReadOnlyList<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context=new ValidationContext<TRequest>(request);
            var results = await Task.WhenAll(_validators.Select(validator=>validator.ValidateAsync(context)));
            
            var failures=results.Where(result=>!result.IsValid).ToList();
            
            
            return await next();
        }
    }
}
