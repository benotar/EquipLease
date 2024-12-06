using EquipLease.Application.Interfaces.Persistence;
using EquipLease.Domain.Entities;
using EquipLease.Persistence.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;

namespace EquipLease.Persistence;

public class EquipDbContext(DbContextOptions<EquipDbContext> options) : DbContext(options), IDbContext
{
    public DbSet<ProductionFacility> ProductionFacilities { get; set; }
    public DbSet<ProcessEquipmentType> ProcessEquipmentTypes { get; set; }
    public DbSet<EquipmentPlacementContract> EquipmentPlacementContracts { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfiguration(new ProductionFacilityTypeConfiguration());
        modelBuilder.ApplyConfiguration(new ProcessEquipmentTypeConfiguration());
        modelBuilder.ApplyConfiguration(new EquipmentPlacementContractTypeConfiguration());

        // Initial data for ProductionFacility
        modelBuilder.Entity<ProductionFacility>().HasData(
            new ProductionFacility
            {
                Id = 1,
                Code = "PF001",
                Name = "Facility 1",
                StandardAreaForEquipment = 1000.00m
            },
            new ProductionFacility
            {
                Id = 2,
                Code = "PF002",
                Name = "Facility 2",
                StandardAreaForEquipment = 1500.00m
            },
            new ProductionFacility
            {
                Id = 3,
                Code = "PF003",
                Name = "Facility 3",
                StandardAreaForEquipment = 2000.00m
            }
        );

        // Initial data for ProcessEquipmentType
        modelBuilder.Entity<ProcessEquipmentType>().HasData(
            new ProcessEquipmentType
            {
                Id = 1,
                Code = "PET001",
                Name = "Equipment Type 1",
                Area = 50.00m
            },
            new ProcessEquipmentType
            {
                Id = 2,
                Code = "PET002",
                Name = "Equipment Type 2",
                Area = 75.00m
            },
            new ProcessEquipmentType
            {
                Id = 3,
                Code = "PET003",
                Name = "Equipment Type 3",
                Area = 100.00m
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}