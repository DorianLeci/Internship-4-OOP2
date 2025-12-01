using FluentValidation;
using Internship_4_OOP.Application.Abstractions;

namespace Internship_4_OOP.Application.Companies.Commands.UpdateCompany;

public class UpdateCompanyValidator:AbstractValidator<UpdateCompanyCommand>
{
    public UpdateCompanyValidator()
    {
        CompanyValidationRules.ApplyRules(this);
    }
}