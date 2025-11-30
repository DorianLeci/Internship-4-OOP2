using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.ActivateUser;

public record ActivateUserCommand(int Id) : IRequest<Result<int, IDomainError>>;


public class ActivateUserCommandHandler(
    IUserRepository userRepository,
    IMediator mediator,
    IUserDbContext dbContext) : IRequestHandler<ActivateUserCommand, Result<int, IDomainError>>
{
    public async Task<Result<int, IDomainError>> Handle(ActivateUserCommand request, CancellationToken cancellationToken)
    {
        var user=await userRepository.GetByIdAsyncWithCore(request.Id);
        
        if (user==null)
            return Result<int,IDomainError>.Failure(DomainError.NotFound("Korisnik s unesenim id-om nije pronađen"));

        if (user.IsActive)
            return Result<int,IDomainError>.Failure(DomainError.Conflict("Korisnik je već aktivan"));
        
        user.IsActive = true;
        await dbContext.SaveChangesAsync(cancellationToken);
        
        return Result<int, IDomainError>.Success(user.Id);

    }
    
}