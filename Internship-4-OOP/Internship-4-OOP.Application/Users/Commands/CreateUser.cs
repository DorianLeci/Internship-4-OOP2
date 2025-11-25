using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Events;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands;
public record CreateUserCommand: IRequest<int>
{
    public int Id{get; set;}
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AddressStreet{get; set;}
    public string AddressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website;
    private string _password = Guid.NewGuid().ToString();
    public bool IsActive = true;
}

public class CreateUserCommandHandler(IApplicationDbContext context) : IRequestHandler<CreateUserCommand,int>
{
    private readonly IApplicationDbContext _context = context;

    public async Task<int> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        var newUser = new User(request.Id,request.Name,request.Username,request.Email,request.AddressStreet,request.AddressCity
        ,request.GeoLatitude,request.GeoLongitude,request.Website);

        newUser.AddDomainEvent(new UserCreatedEvent(newUser));
        
        _context.Users.Add(newUser);
        var id=await _context.SaveChangesAsync(cancellationToken);

        return id;

    }
}

