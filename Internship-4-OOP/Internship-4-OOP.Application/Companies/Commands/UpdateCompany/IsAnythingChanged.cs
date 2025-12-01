using Internship_4_OOP.Domain.Entities.Company;

namespace Internship_4_OOP.Application.Companies.Commands.UpdateCompany;

public static class IsAnythingChanged
{
    public static bool CompanyChanged(Company company,UpdateCompanyCommand request)
    {
        return company.Name != request.CompanyName;
    }
}