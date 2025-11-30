
using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;
using Internship_4_OOP.Infrastructure.Database.Configuration.Companies;
using Internship_4_OOP.Infrastructure.Database.Configuration.Users;
using Internship_4_OOP.Infrastructure.Manager;
using Internship_4_OOP.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;


namespace Internship_4_OOP.Infrastructure.Dependencies;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        AddServices(services);
        return AddDbContext(services, configuration);
    }

    private static void AddServices(this IServiceCollection services)
    {
        services.AddScoped<IDapperManager<User>>(sp =>
            new UserDapperManager(sp.GetRequiredService<IConfiguration>().GetConnectionString("UserDB")!));

        services.AddScoped<IDapperManager<Company>>(sp =>
            new CompanyDapperManager(sp.GetRequiredService<IConfiguration>().GetConnectionString("CompanyDB")!));
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ICompanyRepository, CompanyRepository>();
        services.AddScoped<IUserDbContext>(provider => provider.GetRequiredService<UserDbContext>());
        services.AddScoped<ICompanyDbContext>(provider => provider.GetRequiredService<CompanyDbContext>());
    }

    private static IServiceCollection AddDbContext(this IServiceCollection services, IConfiguration configuration)
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
        
        services.AddDbContext<UserDbContext>(options => options.UseNpgsql(userDbConnectionString));
        services.AddDbContext<CompanyDbContext>(options => options.UseNpgsql(companyDbConnectionString));
        
    }
}