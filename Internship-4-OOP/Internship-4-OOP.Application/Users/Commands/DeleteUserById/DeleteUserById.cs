using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Events.User;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Users.Commands.DeleteUserById;

public record DeleteUserByIdCommand(int Id) : IRequest<Result<int, DomainError>>;


    
    public class DeleteUserByIdCommandHandler(IUserRepository repository,IMediator mediator) : IRequestHandler<DeleteUserByIdCommand, Result<int, DomainError>>
    {
        public async Task<Result<int, DomainError>> Handle(DeleteUserByIdCommand request,
            CancellationToken cancellationToken)
        {
            var deleteUser=await  repository.DeleteAsync(request.Id);
            if (deleteUser == null)
                return Result<int, DomainError>.Failure(DomainError.NotFound("Korisnik kojeg si zatražio da obrišeš po id-u ne postoji u bazi podataka."));   
            
            deleteUser.AddDomainEvent(new UserDeletedEvent(3,"UserDeletedEvent",deleteUser.Id,DateTimeOffset.Now,deleteUser));

            await mediator.Publish(deleteUser.DomainEvents.Last());
            
            return  Result<int, DomainError>.Success(deleteUser.Id);
        }

    }