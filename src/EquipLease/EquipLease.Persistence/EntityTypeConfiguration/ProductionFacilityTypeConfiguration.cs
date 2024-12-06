using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipLease.Persistence.EntityTypeConfiguration;

public class ProductionFacilityTypeConfiguration : IEntityTypeConfiguration<ProductionFacility>
{
    public void Configure(EntityTypeBuilder<ProductionFacility> builder)
    {
        builder.ToTable("ProductionFacilities");
        
        builder.Property(pf => pf.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pf => pf.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pf => pf.StandardAreaForEquipment)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}