namespace Internship_4_OOP.Domain.Common.Exceptions;

public class ValidationException:Exception
{
    public IReadOnlyList<string> Errors { get; }
    
    public ValidationException(List<string>errors)
    {
        Errors = errors ?? new List<string>();
    }

    public ValidationException(string message,List<string>errors) : base(message)
    {
        Errors = errors ?? new List<string>();
    }

    public ValidationException(string message, Exception innerException) : base(message, innerException)
    {
        Errors=new List<string>(){innerException.Message};
    }
}