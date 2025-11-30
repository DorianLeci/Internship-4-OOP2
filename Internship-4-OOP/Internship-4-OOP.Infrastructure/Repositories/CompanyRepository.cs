using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Infrastructure.Database.Configuration.Companies;
using Internship_4_OOP.Infrastructure.Manager;
using Microsoft.EntityFrameworkCore;

namespace Internship_4_OOP.Infrastructure.Repositories;

public class CompanyRepository(CompanyDbContext context,IDapperManager<Company> dapperManager):Repository<Company,int>(context,dapperManager),ICompanyRepository
{
    public async Task<bool> ExistsByNameAsync(string name)
    {
        return await DbSet.AnyAsync(company=>company.Name==name);        
    }
    public async Task<bool> CompanyIdExistsAsync(int companyId)
    {
        return await context.Companies.AnyAsync(c => c.Id == companyId);
    }
}