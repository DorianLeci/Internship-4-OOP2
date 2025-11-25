using System.Net.Mail;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.RuleBuilder;
using FluentValidation;

public static class FluentValidationExtensions
{
    
    public static IRuleBuilderOptions<T,TProperty> Required<T,TProperty>
        (this IRuleBuilder<T,TProperty> ruleBuilder,string displayName)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage("${displayName} ne smije biti prazno.")
            .WithSeverity(Severity.Error);
    }

    public static IRuleBuilderOptions<T, string> MaxLength<T>
        (this IRuleBuilder<T,string> ruleBuilder,string displayName,int maxLength)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithMessage("${displayName} ne smije imati više od {maxLength} znakova.")
            .WithSeverity(Severity.Error);
    }
    
    public static IRuleBuilder<T,string> EmailValidator<T>
        (this IRuleBuilder<T,string> ruleBuilder,string displayName,IUserRepository repository)
    {
        return ruleBuilder
            .Must(email =>
            {
                try
                {
                    var mailAddress = new MailAddress(email);
                    return true;
                }
                catch (Exception e)
                {
                    return false;
                }
            })
            .WithMessage($"{displayName} je krivog formata.")
            .WithSeverity(Severity.Error)
            
            .MustAsync(async (email,CancellationToken) => !await repository.ExistsByEmailAsync(email))
            .WithMessage("${displayName} mora biti unikatan.")
            .WithSeverity(Severity.Error);
    }

    public static IRuleBuilderOptions<T, string> UsernameValidator<T>(
        this IRuleBuilder<T, string> ruleBuilder, string displayName, IUserRepository repository)
    {
        return ruleBuilder
            .MustAsync(async (username,CancellationToken) => !await repository.ExistsByEmailAsync(username))
            .WithMessage("${displayName} mora biti unikatan.")
            .WithSeverity(Severity.Error);
    }

    public static IRuleBuilderOptions<T, decimal> GeoCoordValidator<T>(
        this IRuleBuilder<T, decimal> ruleBuilder, string displayName,decimal lowerBound,decimal upperBound)
    {
        return ruleBuilder
            .InclusiveBetween(lowerBound,upperBound)
            .WithMessage($"{displayName} mora biti izmedu {lowerBound} i {upperBound} stupnjeva.")
            .WithSeverity(Severity.Error);
    }
    public static IRuleBuilder<T,string?> WebsiteUrlValidator<T>
        (this IRuleBuilder<T,string?> ruleBuilder,string? websiteUrl)
    {
        return ruleBuilder
            .Must(url=>string.IsNullOrEmpty(url) || Uri.TryCreate(url, UriKind.Absolute,out _))
            .WithMessage("${websiteUrl} mora biti ispravan.")
            .WithSeverity(Severity.Error);
    }
}