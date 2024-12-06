using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipLease.Persistence.EntityTypeConfiguration;

public class ProcessEquipmentTypeConfiguration : IEntityTypeConfiguration<ProcessEquipmentType>
{
    public void Configure(EntityTypeBuilder<ProcessEquipmentType> builder)
    {
        builder.ToTable("ProcessEquipmentTypes");
        
        builder.Property(pet => pet.Code)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(pet => pet.Name)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(pet => pet.Area)
            .IsRequired()
            .HasPrecision(18, 2);
    }
}