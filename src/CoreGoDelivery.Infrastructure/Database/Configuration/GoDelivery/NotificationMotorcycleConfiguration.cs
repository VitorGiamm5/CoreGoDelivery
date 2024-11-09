using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class NotificationMotorcycleConfiguration : IEntityTypeConfiguration<NotificationMotorcycleEntity>
{
    public void Configure(EntityTypeBuilder<NotificationMotorcycleEntity> entity)
    {
        entity.ToTable("tb_notificationMotorcycle");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnName("ID_NOTIFICATION");
        entity.Property(e => e.IdMotorcycle).HasColumnName("ID_MOTORCYCLE");
        entity.Property(e => e.YearManufacture).HasColumnName("YEAR_MANUFACTURE");

        entity.Property(e => e.DateCreated).HasColumnName("DATE_CREATED").HasColumnType("timestamptz");
        entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
    }
}
