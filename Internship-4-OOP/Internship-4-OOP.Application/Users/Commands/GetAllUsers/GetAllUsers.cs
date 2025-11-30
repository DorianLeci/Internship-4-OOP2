using AutoMapper;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Commands.GetUserById;
using Internship_4_OOP.Application.Users.Mappers;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands.GetAllUsers;

public record GetAllUsersQuery() : IQuery<List<GetUserDto>>
{
}

public class GetUserByIdQueryHandler(IUserRepository repository) : IQueryHandler<GetAllUsersQuery, List<GetUserDto>>
{
    public async Task<Result<List<GetUserDto>, IDomainError>> Handle(GetAllUsersQuery request,
        CancellationToken cancellationToken)
    {
        var users = await repository.GetAllAsync();
        if (!users.Any())
            return Result<List<GetUserDto>, IDomainError>.Failure(DomainError.NotFound("Ne postoji niti jedan korisnik u bazi podataka."));
            
        var usersDtoList = users.Select(UserMapper.GetDtoFromUser).ToList();
            
        return  Result<List<GetUserDto>, IDomainError>.Success(usersDtoList);
    }
        
}