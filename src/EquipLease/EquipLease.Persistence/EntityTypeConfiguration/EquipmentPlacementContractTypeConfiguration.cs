using EquipLease.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EquipLease.Persistence.EntityTypeConfiguration;

public class EquipmentPlacementContractTypeConfiguration : IEntityTypeConfiguration<EquipmentPlacementContract>
{
    public void Configure(EntityTypeBuilder<EquipmentPlacementContract> builder)
    {
        builder.ToTable("EquipmentPlacementContracts");

        builder.Property(epc => epc.NumberOfEquipmentUnits)
            .IsRequired();

        builder.HasOne(epc => epc.ProductionFacility)
            .WithMany(pf => pf.EquipmentPlacementContracts)
            .HasForeignKey(epc => epc.ProductionFacilityId);

        builder.HasOne(epc => epc.ProcessEquipmentType)
            .WithMany(pet => pet.EquipmentPlacementContracts)
            .HasForeignKey(epc => epc.ProcessEquipmentTypeId);
    }
}