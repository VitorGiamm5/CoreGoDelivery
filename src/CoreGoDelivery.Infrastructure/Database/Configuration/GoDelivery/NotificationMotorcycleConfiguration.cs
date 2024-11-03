using CoreGoDelivery.Domain.Entities.GoDelivery.NotificationMotorcycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class NotificationMotorcycleConfiguration : IEntityTypeConfiguration<NotificationMotorcycleEntity>
{
    public void Configure(EntityTypeBuilder<NotificationMotorcycleEntity> builder)
    {
        builder.ToTable("tb_notificationMotorcycle");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_NOTIFICATION");
        builder.Property(t => t.IdMotorcycle).HasColumnName("ID_MOTORCYCLE");
        builder.Property(t => t.YearManufacture).HasColumnName("YEAR_MANUFACTURE");
        builder.Property(t => t.CreatedAt).HasColumnName("DATE_CREATED");
    }
}
