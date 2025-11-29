using Internship_4_OOP.Domain.Entities.Company;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Internship_4_OOP.Infrastructure.Database.Companies;

internal sealed class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {   
            builder.ToTable("companies");
            builder.HasKey(company => company.Id);
            builder.Property(company => company.Id).HasColumnName("Id");
            builder.Property(company=>company.Name).HasColumnName("name");

        }
    }
