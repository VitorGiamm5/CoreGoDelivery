using CoreGoDelivery.Domain.Entities.GoDelivery.Rental;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    internal class RentalConfiguration : IEntityTypeConfiguration<RentalEntity>
    {
        public void Configure(EntityTypeBuilder<RentalEntity> builder)
        {
            builder.ToTable("tb_rental");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("ID_RENTAL");
            builder.Property(t => t.StartDate).HasColumnName("DATE_START");
            builder.Property(t => t.EndDate).HasColumnName("DATE_END");
            builder.Property(t => t.EstimatedReturnDate).HasColumnName("DATE_ESTIMATED_RETURN");
            builder.Property(t => t.ReturnedToBaseDate).HasColumnName("DATE_RETURNED_TO_BASE");
            builder.Property(t => t.DeliverierId).HasColumnName("ID_FK_DELIVERIER");
            builder.Property(t => t.MotorcycleId).HasColumnName("ID_FK_MOTOCYCLE");
            builder.Property(t => t.RentalPlanId).HasColumnName("ID_FK_RENTAL_PLAN");
        }
    }
}
