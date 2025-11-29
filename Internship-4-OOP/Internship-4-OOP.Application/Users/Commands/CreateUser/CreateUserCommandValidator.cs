using FluentValidation;
using Internship_4_OOP.Application.RuleBuilder;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands.CreateUser;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator(IUserRepository repository)
    {
        const string nameReq = "Ime korisnika";
        const string usernameReq = "Korisničko ime ";
        const string emailVal = "Email";
        const string adressStreetVal = "Ulična adresa";
        const string adressCityVal = "Adresa grada";
        const string geoLatVal = "Geografska širina";
        const string geoLongVal = "Geografska dužina";
        const string webSiteVal = "Web stranica";

        RuleFor(request => request.Name).Required(nameReq).DependentRules(()=>RuleFor(request=>request.Name).MaxLength(nameReq, 100));

        RuleFor(request => request.Username).Required(usernameReq)
            .DependentRules(() => RuleFor(request => request.Username).MaxLength(usernameReq, 30));

        RuleFor(request => request.Email).Required(emailVal).DependentRules(() =>
        {
            RuleFor(request => request.Email).MaxLength(emailVal, 150);
            RuleFor(request => request.Email).EmailValidator(emailVal);
        });


        RuleFor(request => request.AddressStreet).Required(adressStreetVal)
            .DependentRules(() => RuleFor(request => request.AddressStreet).MaxLength(adressStreetVal, 150));
        
        RuleFor(request=>request.AddressCity).Required(adressCityVal)
            .DependentRules(() => RuleFor(request => request.AddressCity).MaxLength(adressCityVal,100));

        RuleFor(request => request.GeoLatitude).Required(geoLatVal)
            .DependentRules(() => RuleFor(request => request.GeoLatitude).GeoCoordValidator(geoLatVal, -90m, 90m));

        RuleFor(request => request.GeoLongitude).Required(geoLongVal)
            .DependentRules(() => RuleFor(request => request.GeoLongitude).GeoCoordValidator(geoLongVal, -180m, 180m));

        RuleFor(request => request.Website).MaxLengthForWebsite(webSiteVal,100).WebsiteUrlValidator(webSiteVal);

    }

        
}
