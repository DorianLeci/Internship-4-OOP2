using FluentValidation;
using Internship_4_OOP.Application.Users.Commands;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace Internship_4_OOP.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IValidator<CreateUserCommand>, CreateUserCommandValidator>();
    }
}