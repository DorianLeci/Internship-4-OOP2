using AutoMapper;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Mappers;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands.GetUserById;

public record GetUserByIdQuery(int Id) : IQuery<GetUserDto>
{
    public static GetUserByIdQuery FromId(int id)
    {
        return new GetUserByIdQuery(id);
    }
}
    
    public class GetUserByIdQueryHandler(IUserRepository repository) : IQueryHandler<GetUserByIdQuery, GetUserDto>
    {
        public async Task<Result<GetUserDto, IDomainError>> Handle(GetUserByIdQuery request,
            CancellationToken cancellationToken)
        {
            var user=await  repository.GetByIdAsync(request.Id);
            if (user == null)
                return Result<GetUserDto, IDomainError>.Failure(DomainError.NotFound("Korisnik kojeg si zatražio po id-u ne postoji u bazi podataka."));
            
            var userDto = UserMapper.GetDtoFromUser(user);
            
            return  Result<GetUserDto, IDomainError>.Success(userDto);
        }
        
    }