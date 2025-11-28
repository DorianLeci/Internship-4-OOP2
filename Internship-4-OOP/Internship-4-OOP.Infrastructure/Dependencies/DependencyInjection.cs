using FluentValidation;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Application.Users.Commands.DeleteUserById;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Infrastructure.Database;
using Internship_4_OOP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Internship_4_OOP.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, UserRepository>();
    }

    public static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
    {
        AddDatabase(services, configuration);
        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        var userDbConnectionString = configuration.GetConnectionString("UserDB");
        if (string.IsNullOrEmpty(userDbConnectionString))
            throw new ArgumentNullException(nameof(userDbConnectionString));
        
        var companyDbConnectionString = configuration.GetConnectionString("CompanyDB");
        if (string.IsNullOrEmpty(companyDbConnectionString))
            throw new ArgumentNullException(nameof(companyDbConnectionString));
        
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(userDbConnectionString));
        services.AddDbContext<ApplicationDbContext>(options => options.UseNpgsql(companyDbConnectionString));
        
    }
}