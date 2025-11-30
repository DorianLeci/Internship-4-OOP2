using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Domain.Entities.Users;

namespace Internship_4_OOP.Application.Users;
using AutoMapper;

public class UserProfile:Profile
{
    public UserProfile()
    {
        CreateMap<User, CreateUserDto>();

    }
}