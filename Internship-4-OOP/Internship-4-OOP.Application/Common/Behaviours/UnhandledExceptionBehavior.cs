using FluentValidation;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class UnhandledExceptionBehavior<TRequest, TResponse>(ILogger<TRequest> logger)
    : IPipelineBehavior<TRequest, TResponse>
    where TRequest : notnull
{
    private readonly ILogger<TRequest> _logger = logger;

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (ValidationException e)
        {
            var domainError=DomainError.Validation(e.Message,e.Errors.ToList());
            var failureResult = Result<int, DomainError>.Failure(domainError);
            
            _logger.LogError(e, "Zahtjev: neuspješna validacija: {Name} {@request}", typeof(TRequest).Name, request);
            
            if (failureResult is TResponse response)
                return response;

            throw new InvalidCastException("Response je pogrešno castan.");

        }
        catch (Exception e)
        {
            var domainError = DomainError.Unexpected(e.Message);
            var failureResult = Result<int, DomainError>.Failure(domainError);
            
            _logger.LogError(e, "Zahtjev: neobrađena iznimka: {Name} {@request}", typeof(TRequest).Name, request);

            if (failureResult is TResponse response)
                return response;
            
            throw new InvalidCastException("Response je pogrešno castan.");

        }
        
    }
}