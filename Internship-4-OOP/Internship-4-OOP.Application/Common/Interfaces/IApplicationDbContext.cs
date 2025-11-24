using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<User> Users { get; set; }
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}