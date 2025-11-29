using Internship_4_OOP.Domain.Common.Base;

namespace Internship_4_OOP.Domain.Entities.Company;

public class Company:BaseEntity<Company>
{
    public Company(int id, string name) : base(name){}
    
    
}