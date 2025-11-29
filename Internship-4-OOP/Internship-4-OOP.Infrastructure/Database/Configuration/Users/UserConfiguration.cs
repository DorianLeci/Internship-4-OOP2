using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {   
        builder.ToTable("users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("id");
        builder.Property(user=>user.Name).HasColumnName("name");
        builder.Property(user => user.Email).HasColumnName("email");
        builder.Property(user=>user.AddressStreet).HasColumnName("address_street");
        builder.Property(user => user.AddressCity).HasColumnName("address_city");
        builder.Property(user=>user.GeoLatitude).HasColumnName("geo_lat");
        builder.Property(user => user.GeoLongitude).HasColumnName("geo_lng");
        builder.Property(user=>user.Website).HasColumnName("website");
        builder.Property(user=>user.Password).HasColumnName("password");
        builder.Property(user=>user.CreatedAt).HasColumnName("created_at");   
        builder.Property(user=>user.UpdatedAt).HasColumnName("updated_at");   
        builder.Property(user=>user.IsActive).HasColumnName("is_active");
    }
}