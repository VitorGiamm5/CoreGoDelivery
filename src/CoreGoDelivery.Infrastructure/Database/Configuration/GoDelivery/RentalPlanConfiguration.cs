using CoreGoDelivery.Domain.Entities.GoDelivery.RentalPlan;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class RentalPlanConfiguration : IEntityTypeConfiguration<RentalPlanEntity>
{
    public void Configure(EntityTypeBuilder<RentalPlanEntity> builder)
    {
        builder.ToTable("tb_rentalPlan");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_RENTAL_PLAN");
        builder.Property(t => t.DayliCost).HasColumnName("DAYLI_COST");
        builder.Property(t => t.DaysQuantity).HasColumnName("DAYS_QUANTITY");
    }
}
