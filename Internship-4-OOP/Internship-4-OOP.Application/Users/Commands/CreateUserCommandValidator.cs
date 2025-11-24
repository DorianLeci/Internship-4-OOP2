using FluentValidation;
using FluentValidation.Validators;

namespace Internship_4_OOP.Application.Users.Commands;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(request => request.Name).MaximumLength(256);
    }
}