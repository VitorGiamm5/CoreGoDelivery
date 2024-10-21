﻿// <auto-generated />
using System;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20241016193004_InicialBase")]
    partial class InicialBase
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasDefaultSchema("dbgodelivery")
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier.DeliverierEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_DELIVERIER");

                    b.Property<DateTime>("BirthDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_BIRTH");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CNPJ");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FULL_NAME");

                    b.Property<string>("LicenceDriverId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_LICENSE_DRIVER");

                    b.HasKey("Id");

                    b.HasIndex("LicenceDriverId");

                    b.ToTable("tb_deliverier", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver.LicenceDriverEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_LICENSE_DRIVER");

                    b.Property<string>("ImageUrlReference")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("IMAGE_URL_REFERENCE");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("ID_LICENSE_TYPE");

                    b.HasKey("Id");

                    b.ToTable("tb_licenceDriver", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle.ModelMotorcycleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_MODEL_MOTORCYCLE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NORMALIZED_NAME");

                    b.HasKey("Id");

                    b.ToTable("tb_modelMotorcycle", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle.MotorcycleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_MOTOCYCLE");

                    b.Property<string>("ModelMotorcycleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_MODEL_MOTOCYCLE");

                    b.Property<string>("PlateNormalized")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("PLATE_NORMALIZED");

                    b.Property<int>("YearManufacture")
                        .HasColumnType("integer")
                        .HasColumnName("YEAR_MANUFACTURE");

                    b.HasKey("Id");

                    b.ToTable("tb_motocycle", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Rental.RentalEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_RENTAL");

                    b.Property<string>("DeliverierId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_DELIVERIER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_END");

                    b.Property<DateTime>("EstimatedReturnDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_ESTIMATED_RETURN");

                    b.Property<string>("MotorcycleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_MOTOCYCLE");

                    b.Property<int>("RentalPlanId")
                        .HasColumnType("integer")
                        .HasColumnName("ID_FK_RENTAL_PLAN");

                    b.Property<DateTime?>("ReturnedToBaseDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_RETURNED_TO_BASE");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_START");

                    b.HasKey("Id");

                    b.HasIndex("DeliverierId");

                    b.HasIndex("MotorcycleId");

                    b.HasIndex("RentalPlanId");

                    b.ToTable("tb_rental", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan.RentalPlanEntity", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer")
                        .HasColumnName("ID_RENTAL_PLAN");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("Id"));

                    b.Property<double>("DayliCost")
                        .HasColumnType("double precision")
                        .HasColumnName("DAYLI_COST");

                    b.Property<int>("DaysQuantity")
                        .HasColumnType("integer")
                        .HasColumnName("DAYS_QUANTITY");

                    b.HasKey("Id");

                    b.ToTable("tb_rentalPlan", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier.DeliverierEntity", b =>
                {
                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver.LicenceDriverEntity", "LicenceDriver")
                        .WithMany()
                        .HasForeignKey("LicenceDriverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LicenceDriver");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Rental.RentalEntity", b =>
                {
                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier.DeliverierEntity", "Deliverier")
                        .WithMany()
                        .HasForeignKey("DeliverierId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle.MotorcycleEntity", "Motorcycle")
                        .WithMany()
                        .HasForeignKey("MotorcycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan.RentalPlanEntity", "RentalPlan")
                        .WithMany()
                        .HasForeignKey("RentalPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverier");

                    b.Navigation("Motorcycle");

                    b.Navigation("RentalPlan");
                });
#pragma warning restore 612, 618
        }
    }
}
