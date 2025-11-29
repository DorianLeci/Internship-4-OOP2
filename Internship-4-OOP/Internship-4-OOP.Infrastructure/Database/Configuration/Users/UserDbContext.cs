using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Users;

public class UserDbContext:DbContext,IApplicationDbContext
{
    public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
    {
    }
    public DbSet<User> Users { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserConfiguration).Assembly);
    }
}