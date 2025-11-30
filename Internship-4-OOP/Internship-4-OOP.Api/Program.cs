using System.Reflection;
using Dapper.FluentMap;
using Internship_4_OOP.Application.Dependencies;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Infrastructure.Dependencies;

namespace Internship_4_OOP.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen();
        builder.Services.AddAppServices();
        builder.Services.AddInfrastructure(builder.Configuration);
        builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
        
        FluentMapper.Initialize(cfg=>cfg.AddMap(new UserMap()));
        var app = builder.Build();
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.RoutePrefix = "swagger";
            });
        }
        app.UseHttpsRedirection();
        app.MapControllers();
        app.Run();
    }


}