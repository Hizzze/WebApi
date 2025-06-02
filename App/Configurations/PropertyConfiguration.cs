using App.Models.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Configurations;

public class PropertyConfiguration : IEntityTypeConfiguration<Property>
{
    public void Configure(EntityTypeBuilder<Property> builder)
    {
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).HasMaxLength(100).IsRequired();
        builder.Property(x => x.Description).HasMaxLength(3500).IsRequired();
        builder.Property(x => x.Price).IsRequired();
        builder.Property(x => x.Location).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ContactEmail).HasMaxLength(100).IsRequired();
        builder.Property(x => x.ContactPhone).HasMaxLength(15).IsRequired();

        builder.HasOne(x => x.Owner)
            .WithMany(x => x.Properties)
            .HasForeignKey(x => x.OwnerId)
            .OnDelete(DeleteBehavior.Cascade);
        builder.HasOne(x => x.Details)
            .WithOne(d => d.Property)
            .HasForeignKey<PropertyDetails>(x => x.PropertyId)
            .OnDelete(DeleteBehavior.Cascade);
        
        
    }
}