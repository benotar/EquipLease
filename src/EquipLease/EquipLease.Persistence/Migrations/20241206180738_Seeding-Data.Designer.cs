﻿// <auto-generated />
using System;
using EquipLease.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace EquipLease.Persistence.Migrations
{
    [DbContext(typeof(EquipDbContext))]
    [Migration("20241206180738_Seeding-Data")]
    partial class SeedingData
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("EquipLease.Domain.Entities.EquipmentPlacementContract", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<int>("NumberOfEquipmentUnits")
                        .HasColumnType("int");

                    b.Property<int>("ProcessEquipmentTypeId")
                        .HasColumnType("int");

                    b.Property<int>("ProductionFacilityId")
                        .HasColumnType("int");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.HasIndex("ProcessEquipmentTypeId");

                    b.HasIndex("ProductionFacilityId");

                    b.ToTable("EquipmentPlacementContracts", (string)null);
                });

            modelBuilder.Entity("EquipLease.Domain.Entities.ProcessEquipmentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<decimal>("Area")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProcessEquipmentTypes", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Area = 50.00m,
                            Code = "PET001",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3714),
                            Name = "Equipment Type 1",
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3715)
                        },
                        new
                        {
                            Id = 2,
                            Area = 75.00m,
                            Code = "PET002",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3719),
                            Name = "Equipment Type 2",
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3720)
                        },
                        new
                        {
                            Id = 3,
                            Area = 100.00m,
                            Code = "PET003",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3722),
                            Name = "Equipment Type 3",
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3723)
                        });
                });

            modelBuilder.Entity("EquipLease.Domain.Entities.ProductionFacility", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Code")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime2");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<decimal>("StandardAreaForEquipment")
                        .HasPrecision(18, 2)
                        .HasColumnType("decimal(18,2)");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("ProductionFacilities", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Code = "PF001",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3490),
                            Name = "Facility 1",
                            StandardAreaForEquipment = 1000.00m,
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3495)
                        },
                        new
                        {
                            Id = 2,
                            Code = "PF002",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3505),
                            Name = "Facility 2",
                            StandardAreaForEquipment = 1500.00m,
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3506)
                        },
                        new
                        {
                            Id = 3,
                            Code = "PF003",
                            CreatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3509),
                            Name = "Facility 3",
                            StandardAreaForEquipment = 2000.00m,
                            UpdatedAt = new DateTime(2024, 12, 6, 18, 7, 38, 113, DateTimeKind.Utc).AddTicks(3509)
                        });
                });

            modelBuilder.Entity("EquipLease.Domain.Entities.EquipmentPlacementContract", b =>
                {
                    b.HasOne("EquipLease.Domain.Entities.ProcessEquipmentType", "ProcessEquipmentType")
                        .WithMany("EquipmentPlacementContracts")
                        .HasForeignKey("ProcessEquipmentTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("EquipLease.Domain.Entities.ProductionFacility", "ProductionFacility")
                        .WithMany("EquipmentPlacementContracts")
                        .HasForeignKey("ProductionFacilityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ProcessEquipmentType");

                    b.Navigation("ProductionFacility");
                });

            modelBuilder.Entity("EquipLease.Domain.Entities.ProcessEquipmentType", b =>
                {
                    b.Navigation("EquipmentPlacementContracts");
                });

            modelBuilder.Entity("EquipLease.Domain.Entities.ProductionFacility", b =>
                {
                    b.Navigation("EquipmentPlacementContracts");
                });
#pragma warning restore 612, 618
        }
    }
}
