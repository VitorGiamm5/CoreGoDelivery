using CoreGoDelivery.Domain.Entities.GoDelivery.Motorcycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class MotorcycleConfiguration : IEntityTypeConfiguration<MotorcycleEntity>
{
    public void Configure(EntityTypeBuilder<MotorcycleEntity> builder)
    {
        builder.ToTable("tb_motorcycle");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_MOTORCYCLE");
        builder.Property(t => t.YearManufacture).HasColumnName("YEAR_MANUFACTURE");
        builder.Property(t => t.PlateNormalized).HasColumnName("PLATE_NORMALIZED");
        builder.Property(t => t.ModelMotorcycleId).HasColumnName("ID_FK_MODEL_MOTORCYCLE");
    }
}
