using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Database;

public class ApplicationDbContext : DbContext,IApplicationDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    { }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);
        modelBuilder.HasDefaultSchema(Schemas.Default);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var trackedEntities = ChangeTracker.Entries().Where(e => e.Entity is IAuditableEntity);
        foreach (var entry in trackedEntities)
        {
            var entity=(IAuditableEntity)entry.Entity;
            switch(entry.State)
            {
                case EntityState.Added:
                    entity.SetCreatedAt();
                    break;
                case EntityState.Modified:
                    entity.SetUpdatedAt();
                    break;
                case EntityState.Deleted:
                    entity.SetDeletedAt();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
                
        }

        return await base.SaveChangesAsync(cancellationToken);
    }
    
}