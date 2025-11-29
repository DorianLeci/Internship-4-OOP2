using Internship_4_OOP.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database.Companies;

public class CompanyDbContext:DbContext
{
    public CompanyDbContext(DbContextOptions<CompanyDbContext> options) : base(options)
    {
    }
    public DbSet<Company> Companies { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(CompanyConfiguration).Assembly);
    }
}