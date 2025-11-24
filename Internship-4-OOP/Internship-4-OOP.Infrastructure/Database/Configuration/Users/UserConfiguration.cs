using Internship_4_OOP.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP.Infrastructure.Database.Configuration.Users;

internal sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {   
        builder.ToTable("Users");
        builder.HasKey(user => user.Id);
        builder.Property(user => user.Id).HasColumnName("Id");
        builder.Property(user=>user.Name).HasColumnName("Name");
        builder.Property(user => user.Email).HasColumnName("Email");
        builder.Property(user=>user.AddressStreet).HasColumnName("AddressStreet");
        builder.Property(user => user.AddressCity).HasColumnName("AddressCity");
        builder.Property(user=>user.GeoLatitude).HasColumnName("GeoLatitude");
        builder.Property(user => user.GeoLongitude).HasColumnName("GeoLongitude");
        builder.Property(user=>user.Website).HasColumnName("Website");
    }
}