using AutoMapper;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands.GetUserById;

public record GetUserByIdQuery(int Id) : IQuery<CreateUserDto>
{
    public static GetUserByIdQuery FromId(int id)
    {
        return new GetUserByIdQuery(id);
    }
}
    
    public class GetUserByIdQueryHandler(IUserRepository repository,IMapper mapper) : IQueryHandler<GetUserByIdQuery, CreateUserDto>
    {
        public async Task<Result<CreateUserDto, IDomainError>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user=await  repository.GetById(request.Id);
            if (user == null)
                return Result<CreateUserDto, IDomainError>.Failure(DomainError.NotFound("Korisnik kojeg si zatražio po id-u ne postoji u bazi podataka."));
            
            var userDto = UserMapper.CreateUserDto(user);
            
            return  Result<CreateUserDto, IDomainError>.Success(userDto);
        }
        
    }