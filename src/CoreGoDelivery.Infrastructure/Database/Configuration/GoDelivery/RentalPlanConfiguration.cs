using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlanEntity>
{
    public void Configure(EntityTypeBuilder<RentalPlanEntity> entity)
    {
        entity.ToTable("tb_rentalPlan");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnName("ID_RENTAL_PLAN");
        entity.Property(e => e.DayliCost).HasColumnName("DAYLI_COST");
        entity.Property(e => e.DaysQuantity).HasColumnName("DAYS_QUANTITY");

        entity.Property(e => e.DateCreated).HasColumnName("DATE_CREATED").HasColumnType("timestamptz");
        entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
    }
}
