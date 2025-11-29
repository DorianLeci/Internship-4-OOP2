using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Infrastructure.Database.Configuration.Companies;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class CompanyRepository(CompanyDbContext context):Repository<Company,int>(context),ICompanyRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(company=>company.Name==name);        
    }
}