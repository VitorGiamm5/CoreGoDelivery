using CoreGoDelivery.Domain.Entities.GoDelivery.Motocycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    public class MotocycleConfiguration : IEntityTypeConfiguration<MotocycleEntity>
    {
        public void Configure(EntityTypeBuilder<MotocycleEntity> builder)
        {
            builder.ToTable("tb_motocycle");
            builder.HasKey(t => t.MotocycleId);

            builder.Property(t => t.MotocycleId).HasColumnName("ID_MOTOCYCLE");
            builder.Property(t => t.YearManufacture).HasColumnName("YEAR_MANUFACTURE");
            builder.Property(t => t.PlateIdNormalized).HasColumnName("ID_PLATE_NORMALIZED");
            builder.Property(t => t.ModelMotocycleId).HasColumnName("ID_FK_NODEL_MOTOCYCLE");
        }
    }
}
