using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Common.Events.User;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.CreateUser;

public record CreateUserCommand(
    string Name,
    string Username,
    string Email,
    string AddressStreet,
    string AddressCity,
    decimal GeoLatitude,
    decimal GeoLongitude,
    string? Website,
    int CompanyId
) : IRequest<Result<int, DomainError>>

{
    public static CreateUserCommand FromDto(CreateUserDto dto)
    {
        return new CreateUserCommand(dto.Name,dto.Username,dto.Email,dto.AddressStreet,dto.AddressCity,dto.GeoLatitude,dto.GeoLongitude,dto.Website,dto.CompanyId);
    }
}


public class CreateUserCommandHandler(IUserRepository userRepository,IMediator mediator,IUserDbContext dbContext) : IRequestHandler<CreateUserCommand,Result<int,DomainError>>
{

    public async Task<Result<int,DomainError>> Handle(CreateUserCommand request, CancellationToken cancellationToken)
    {
        if (await userRepository.ExistsByUsernameAsync(request.Username))
        {
            return Result<int, DomainError>.Failure(
                DomainError.Conflict("Već postoji korisnik s istim korisničkim imenom."));
        }
        if (await userRepository.ExistsByEmailAsync(request.Email))
        {
            return Result<int, DomainError>.Failure(DomainError.Conflict("Već postoji korisnik s istim emailom."));
        }

        if (await userRepository.ExistsUserWithinDistanceAsync(request.GeoLatitude, request.GeoLongitude, 3))
        {
            return Result<int, DomainError>.Failure(DomainError.Conflict("Postoji korisnik unutar 3 kilometra od trenutno unesenog."));
        }
        
        var newUser = new User(request.Name,request.Username,request.Email,request.AddressStreet,request.AddressCity,request.GeoLatitude,request.GeoLongitude,request.Website,request.CompanyId);
        
        await userRepository.InsertAsync(newUser);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        newUser.AddDomainEvent(new UserCreatedEvent(1,"UserCreatedEvent",newUser.Id,DateTimeOffset.Now,newUser));
        
        await mediator.Publish(newUser.DomainEvents.Last());

        return Result<int,DomainError>.Success(newUser.Id);

    }
}

