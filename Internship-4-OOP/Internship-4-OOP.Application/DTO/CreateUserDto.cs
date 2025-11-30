using Internship_4_OOP.Domain.Entities.Company;

namespace Internship_4_OOP.Application.DTO;

public class CreateUserDto
{
    public string Name{get; set;}
    public string Username{get; set;}
    public string Email{get; set;}
    public string AddressStreet{get; set;}
    public string AddressCity{get; set;}
    public decimal GeoLatitude{get; set;}
    public decimal GeoLongitude{get; set;}
    public string? Website { get; set; }
    public int CompanyId{get; set;}

}