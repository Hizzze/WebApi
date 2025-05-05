using App.Models.Properties;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Configurations;

public class PropertyDetailsConfiguration : IEntityTypeConfiguration<PropertyDetails>
{
    public void Configure(EntityTypeBuilder<PropertyDetails> builder)
    {
        builder.HasKey(d => d.Id);

        builder.Property(d => d.Area).IsRequired();
        builder.Property(d => d.Rooms).IsRequired();
        builder.Property(d => d.Floor).IsRequired();
        builder.Property(d => d.TotalFloors).IsRequired();

        builder.Property(d => d.HasBalcony).IsRequired();
        builder.Property(d => d.HasFurniture).IsRequired();
    }
}