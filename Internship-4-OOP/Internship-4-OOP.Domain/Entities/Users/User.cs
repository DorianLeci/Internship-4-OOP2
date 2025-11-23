using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Common.Validation;
using Internship_4_OOP.Domain.Common.Validation.ValidationItems;
using Internship_4_OOP.Domain.Persistence.User;

namespace Internship_4_OOP.Domain.Entities.Users;

public class User
{
    public const int NameMaxLength = 100;
    public int Id{get; set;}
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AdressStreet{get; set;}
    public string AdressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website;
    public string Password{get; set;}

    public async Task<Result<bool>> Create(IUserRepository userRepository)
    {
        var validationResult = await CreateOrUpdateValidation();
        if (validationResult.HasErrors)
        {
            return new Result<bool>(false,validationResult);
        }

        await userRepository.InsertAsync(this);
        return new Result<bool>(true, validationResult);
    }

    public async Task<ValidationResult> CreateOrUpdateValidation()
    {
        var validationResult = new ValidationResult();
        if (Name.Length > NameMaxLength)
            validationResult.AddValidationItem(ValidationItems.User.NameMaxLength);
        
        return validationResult;
    }
}