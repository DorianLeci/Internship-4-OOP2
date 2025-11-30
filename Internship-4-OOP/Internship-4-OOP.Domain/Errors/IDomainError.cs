namespace Internship_4_OOP.Domain.Errors;

public interface IDomainError
{
    string? ErrorMessage { get; init; }
    ErrorType ErrorType { get; init; }
    public List<string>? Errors { get; init; }  
}