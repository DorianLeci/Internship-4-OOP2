using Dapper.FluentMap.Mapping;

namespace Internship_4_OOP.Domain.Entities.Users;

public class UserMap:EntityMap<User>
{
    public UserMap()
    {
        Map(u=>u.Id).ToColumn("id");
        Map(u=>u.Name).ToColumn("name");
        Map(u=>u.Username).ToColumn("username");
        Map(u=>u.Email).ToColumn("email");
        Map(u=>u.AddressStreet).ToColumn("address_street");
        Map(u=>u.AddressCity).ToColumn("address_city");
        Map(u=>u.GeoLatitude).ToColumn("geo_lat");
        Map(u=>u.GeoLongitude).ToColumn("geo_lng");
        Map(u=>u.IsActive).ToColumn("is_active");
        Map(u=>u.CreatedAt).ToColumn("created_at");
        Map(u=>u.UpdatedAt).ToColumn("updated_at");
        Map(u=>u.Website).ToColumn("website");
    }
}