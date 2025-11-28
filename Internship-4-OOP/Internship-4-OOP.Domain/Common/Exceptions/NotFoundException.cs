namespace Internship_4_OOP.Domain.Common.Exceptions;

public class NotFoundException:Exception
{
    public NotFoundException() : base(){}
    public NotFoundException(string message) : base(message){}
    
}