using App.Abstractions;
using App.Models;
using App.Database;
using App.Enums;
using App.PasswordHasher;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Configurations;

public class UserConfigurations : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Name).HasMaxLength(25).IsRequired();
        builder.Property(x => x.LastName).HasMaxLength(25).IsRequired();
        builder.Property(x => x.Time).IsRequired();
        builder.Property(x => x.Login).HasMaxLength(25).IsRequired();
        builder.Property(x => x.PasswordHash).HasMaxLength(256).IsRequired();


        builder.Property(x => x.Role)
            .HasConversion<string>()
            .IsRequired();


        builder.HasData(new User
        {
            Id = Guid.Parse("efdaa81f-9f1e-4e3d-8050-245cca05bf4b"),
            Name = "Vlad",
            LastName = "Syzov",
            Time = DateTime.UtcNow,
            Login = "admin",
            PasswordHash = "$2a$11$2G3iM7uLfkhhYIFuqEAlA.fLSX8oHpwCz00IBL747VLZvLYAqqcCO",
            Role = UserRole.Owner
        });
    }
    
    
}