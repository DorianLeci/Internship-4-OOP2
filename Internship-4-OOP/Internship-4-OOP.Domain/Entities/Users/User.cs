using System.ComponentModel.DataAnnotations.Schema;
using Internship_4_OOP.Domain.Common.Base;

namespace Internship_4_OOP.Domain.Entities.Users;
using Company;

public class User : BaseEntity<User>
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
    
    public string Password => _password;

    public bool IsActive = true;
    public User()
    { }
    public User(string name, string username, string email, string addressStreet, string addressCity, decimal geoLatitude, decimal geoLongitude, string? website,
        int? companyId) : base(name)
    {
        Username = username;
        Email = email;
        AddressStreet = addressStreet;
        AddressCity = addressCity;
        GeoLatitude = geoLatitude;
        GeoLongitude = geoLongitude;
        Website = website;
        CompanyId = companyId;
    }

    [NotMapped]
    public int? CompanyId{get; set;}
}