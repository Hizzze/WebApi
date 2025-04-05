using App.Models;
using App.Database;
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
    }
}