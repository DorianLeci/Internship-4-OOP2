using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Persistence.Common;

namespace Internship_4_OOP.Domain.Persistence.Company;

public interface ICompanyRepository:IRepository<Entities.Company.Company,int>
{
    Task<bool>ExistsByNameAsync(string name);
}