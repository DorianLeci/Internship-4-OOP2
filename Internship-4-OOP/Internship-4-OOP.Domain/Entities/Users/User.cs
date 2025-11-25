using Internship_4_OOP.Domain.Common.Base;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Common.Validation;
using Internship_4_OOP.Domain.Common.Validation.ValidationItems;
using Internship_4_OOP.Domain.Events;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Domain.Entities.Users;

public class User:BaseEntity
{
    public const int NameMaxLength = 100;
    
    public string Username{get; set;}
    public string Email{get; set;}
    public string AddressStreet{get; set;}
    public string AddressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website;
    private string _password = Guid.NewGuid().ToString();
    
    public bool IsActive = true;

    public User(int id,string name,string username,string email,string addressStreet,string addressCity,decimal geoLatitude,decimal geoLongitude,string? website):
        base(name,username)
    {
        Username = username;
        Email = email;
        AddressStreet = addressStreet;
        AddressCity = addressCity;
        GeoLatitude = geoLatitude;
        GeoLatitude = geoLatitude;
        Website = website;

    }


    // public async Task<Result<bool>> Create(IUserRepository userRepository)
    // {
    //     var validationResult = await CreateOrUpdateValidation();
    //     if (validationResult.HasErrors)
    //     {
    //         return new Result<bool>(false,validationResult);
    //     }
    //     
    //     AddDomainEvent(new UserCreatedEvent(this));
    //     await userRepository.InsertAsync(this);
    //     return new Result<bool>(true, validationResult);
    // }
    //
    // public async Task<ValidationResult> CreateOrUpdateValidation()
    // {
    //     var validationResult = new ValidationResult();
    //     if (Name.Length > NameMaxLength)
    //         validationResult.AddValidationItem(ValidationItems.User.NameMaxLength);
    //     
    //     return validationResult;
    // }
}