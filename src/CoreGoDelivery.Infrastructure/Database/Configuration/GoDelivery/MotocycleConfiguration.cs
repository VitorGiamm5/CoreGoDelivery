using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    public class MotocycleConfiguration : IEntityTypeConfiguration<MotorcycleEntity>
    {
        public void Configure(EntityTypeBuilder<MotorcycleEntity> builder)
        {
            builder.ToTable("tb_motocycle");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("ID_MOTOCYCLE");
            builder.Property(t => t.YearManufacture).HasColumnName("YEAR_MANUFACTURE");
            builder.Property(t => t.PlateNormalized).HasColumnName("ID_PLATE_NORMALIZED");
            builder.Property(t => t.ModelMotorcycleId).HasColumnName("ID_FK_MODEL_MOTOCYCLE");
        }
    }
}
