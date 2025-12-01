using FluentValidation;
using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.RuleBuilder;
using Internship_4_OOP.Domain.Persistence.Company;

namespace Internship_4_OOP.Application.Companies.Commands.CreateCompany;


public class CreateCompanyCommandValidator: AbstractValidator<CreateCompanyCommand>
{
    public CreateCompanyCommandValidator()
    {
        CompanyValidationRules.ApplyRules(this);
    }

        
}
