using System.ComponentModel.DataAnnotations.Schema;
using Internship_4_OOP.Domain.Common.Base;

namespace Internship_4_OOP.Domain.Entities.Users;
using Company;

public class User(
    string name,
    string username,
    string email,
    string addressStreet,
    string addressCity,
    decimal geoLatitude,
    decimal geoLongitude,
    string? website,
    int? companyId)
    : BaseEntity<User>(name)
{
    public const int NameMaxLength = 100;
    
    public string Username{get; set;} = username;
    public string Email{get; set;} = email;
    public string AddressStreet{get; set;} = addressStreet;
    public string AddressCity{get; set;} = addressCity;
    public decimal GeoLatitude{get; set;} = geoLatitude;
    public decimal GeoLongitude{get; set;} = geoLongitude;
    public string? Website = website;
    
    private string _password = Guid.NewGuid().ToString();
    
    public string Password => _password;

    public bool IsActive = true;
    
    public int? CompanyId{get; set;} = companyId;
}