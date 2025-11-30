using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Events.User;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.DeactivateUser;

public record DeactivateUserCommand(int Id) : IRequest<Result<int, IDomainError>>;

public class UpdateUserCommandHandler(
    IUserRepository userRepository,
    IMediator mediator,
    IUserDbContext dbContext) : IRequestHandler<DeactivateUserCommand, Result<int, IDomainError>>
{
    public async Task<Result<int, IDomainError>> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var user=await userRepository.GetByIdAsyncWithCore(request.Id);
        
        if (user==null)
            return Result<int,IDomainError>.Failure(DomainError.NotFound("Korisnik s unesenim id-om nije pronađen"));
        
        user.IsActive = false;
        await dbContext.SaveChangesAsync(cancellationToken);

        user.AddDomainEvent(new UserCreatedEvent(3, "UserDeactivatedEvent", user.Id, DateTimeOffset.Now, user));

        await mediator.Publish(user.DomainEvents.Last());

        return Result<int, IDomainError>.Success(user.Id);

    }
    
}