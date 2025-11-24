using FluentValidation;
using Internship_4_OOP.Application.Users.Commands;
using Microsoft.Extensions.DependencyInjection;
namespace Internship_4_OOP.Application.Dependencies;

public static class DependencyInjection
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateUserCommandHandler).Assembly));      
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
    }
}
