﻿// <auto-generated />
using System;
using CoreGoDelivery.Infrastructure.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CoreGoDelivery.Infrastructure.Migrations
{
    [DbContext(typeof(AplicationDbContext))]
    partial class AplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

                    b.Property<string>("CNPJ")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("CNPJ");

                    b.Property<string>("FullName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("FULL_NAME");

                    b.Property<string>("LicenseNumberId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_LICENSE_NUMBER");

                    b.HasKey("Id");

                    b.HasIndex("LicenseNumberId");

                    b.ToTable("tb_deliverier", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver.LicenceDriverEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_LICENSE_DRIVER");

                    b.Property<string>("FileNameImageNormalized")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("IMAGE_URL_REFERENCE");

                    b.Property<int>("Type")
                        .HasColumnType("integer")
                        .HasColumnName("ID_LICENSE_TYPE");

                    b.HasKey("Id");

                    b.ToTable("tb_licenceDriver", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle.ModelMotocycleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_MODEL_MOTOCYCLE");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NAME");

                    b.Property<string>("NormalizedName")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("NORMALIZED_NAME");

                    b.HasKey("Id");

                    b.ToTable("tb_modelMotocycle", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle.MotocycleEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_MOTOCYCLE");

                    b.Property<string>("ModelMotocycleId")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_NODEL_MOTOCYCLE");

                    b.Property<string>("PlateIdNormalized")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_PLATE_NORMALIZED");

                    b.Property<int>("YearManufacture")
                        .HasColumnType("integer")
                        .HasColumnName("YEAR_MANUFACTURE");

                    b.HasKey("Id");

                    b.HasIndex("ModelMotocycleId");

                    b.ToTable("tb_motocycle", "dbgodelivery");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Rental.RentalEntity", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("text")
                        .HasColumnName("ID_RENTAL");

                    b.Property<string>("Id")
                        .IsRequired()
                        .HasColumnType("text")
                        .HasColumnName("ID_FK_DELIVERIER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_END");

                    b.Property<DateTime>("EstimatedReturnDate")
                        .HasColumnType("timestamp with time zone")
                        .HasColumnName("DATE_ESTIMATED_RETURN");

                    b.Property<string>("MotocycleId")
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

                    b.HasIndex("Id");

                    b.HasIndex("MotocycleId");

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

                    b.Property<double>("DailyCost")
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
                        .HasForeignKey("LicenseNumberId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("LicenceDriver");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle.MotocycleEntity", b =>
                {
                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle.ModelMotocycleEntity", "ModelMotocycle")
                        .WithMany()
                        .HasForeignKey("ModelMotocycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("ModelMotocycle");
                });

            modelBuilder.Entity("CoreGoDelivery.Domain.Entities.GoDelivery.Rental.RentalEntity", b =>
                {
                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier.DeliverierEntity", "Deliverier")
                        .WithMany()
                        .HasForeignKey("Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle.MotocycleEntity", "Motocycle")
                        .WithMany()
                        .HasForeignKey("MotocycleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan.RentalPlanEntity", "RentalPlan")
                        .WithMany()
                        .HasForeignKey("RentalPlanId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Deliverier");

                    b.Navigation("Motocycle");

                    b.Navigation("RentalPlan");
                });
#pragma warning restore 612, 618
        }
    }
}
