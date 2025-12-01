using FluentValidation;
using Internship_4_OOP.Application.RuleBuilder;

namespace Internship_4_OOP.Application.Abstractions;

public class CompanyValidationRules
{
    public static void ApplyRules<T>(AbstractValidator<T> validator) where T : ICompanyRequest
    {
        const string nameReq = "Ime kompanije";

        validator.RuleFor(request => request.CompanyName).Required(nameReq).DependentRules(()=>
        {
            validator.RuleFor(request => request.CompanyName).MaxLength(nameReq, 150);
        });
    }
}