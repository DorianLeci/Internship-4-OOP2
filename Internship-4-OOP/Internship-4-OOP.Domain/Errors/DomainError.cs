using FluentValidation.Results;
using Internship_4_OOP.Domain.Common.Model;

namespace Internship_4_OOP.Domain.Errors;

public record DomainError
{
    public string? ErrorMessage { get; init; }
    public ErrorType ErrorType { get; init; }
    public List<ValidationFailure>? Errors { get; init; }
    private DomainError(string? message, ErrorType errorType, List<ValidationFailure>? errors = null)
    {
        ErrorMessage = message;
        ErrorType = errorType;
        Errors = errors;
    }
    public static DomainError Conflict(string ? message)=>
        new(message ?? "Dogodio se konflikt s podatcima u bazi podataka.", ErrorType.Conflict);
    
    public static DomainError Validation(string ? message,List<ValidationFailure>? errors=null) =>
        new (message?? "Neuspješna validacija.",ErrorType.Validation,errors);

    public static DomainError Unexpected(string? message)
        => new(message ?? "Neočekivana pogreška se dogodila.",Domain.Errors.ErrorType.Unexecpected);
    
    public static DomainError NotFound(string? message)=>
    new(message ?? "Podatak kojeg si zatražio ne postoji.", ErrorType.NotFound);
}