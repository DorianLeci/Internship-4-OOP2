using System.Reflection;
using FluentValidation;
using Internship_4_OOP.Application.Common.Behaviours;
using Internship_4_OOP.Application.Companies.Commands.CreateCompany;
using Internship_4_OOP.Application.Events;
using Internship_4_OOP.Application.Users.Commands;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Events;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
namespace Internship_4_OOP.Application.Dependencies;

public static class DependencyInjection
{
    public static void AddAppServices(this IServiceCollection services)
    {
        services.AddMediatR(
            cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
                cfg.AddOpenRequestPreProcessor(typeof(LoggingBehavior<>));
                cfg.AddOpenBehavior(typeof(UnhandledExceptionBehavior<,>));
                cfg.AddOpenBehavior(typeof(RequestValidationBehavior<,>));
            });    
        services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCompanyCommandValidator>();
       


    }
}
