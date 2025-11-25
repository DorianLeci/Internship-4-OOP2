using FluentValidation;
using FluentValidation.Validators;
using Internship_4_OOP.Application.RuleBuilder;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Application.Users.Commands;

public class CreateUserCommandValidator: AbstractValidator<CreateUserCommand>
{
    private readonly IUserRepository _repository;

    public CreateUserCommandValidator(IUserRepository repository)
    {
        _repository = repository;
        const string nameReq = "Ime korisnika";
        const string usernameReq = "Korisničko ime ";
        const string emailVal = "Email";
        const string adressStreetVal = "Ulična adresa";
        const string adressCityVal = "Adresa grada";
        const string geoLatVal = "Geografska širina";
        const string geoLongVal = "Geografska dužina";
        const string webSiteVal = "Web stranica";
        
        RuleFor(request => request.Name).Required(nameReq).MaxLength(nameReq,100);
        RuleFor(request => request.Username).Required(usernameReq).MaxLength(usernameReq,30); 
        RuleFor(request => request.Email).Required(emailVal).EmailValidator(emailVal, _repository);
        RuleFor(request=>request.AddressStreet).Required(adressStreetVal).MaxLength(adressStreetVal,150);
        RuleFor(request=>request.AddressCity).Required(adressCityVal).MaxLength(adressCityVal,100);
        RuleFor(request => request.GeoLatitude).Required(geoLatVal).GeoCoordValidator(geoLatVal,-90m,90m);
        RuleFor(request => request.GeoLatitude).Required(geoLongVal).GeoCoordValidator(geoLongVal,-180m,180m);
        RuleFor(request=>request.Website).WebsiteUrlValidator(webSiteVal);
        
    }

        
}
