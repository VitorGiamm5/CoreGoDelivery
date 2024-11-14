using CoreGoDelivery.Domain.Entities.GoDelivery.MotorcycleModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class MotorcycleModelConfiguration : IEntityTypeConfiguration<MotorcycleModelEntity>
{
    public void Configure(EntityTypeBuilder<MotorcycleModelEntity> entity)
    {
        entity.ToTable("tb_modelMotorcycle");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnName("ID_MODEL_MOTORCYCLE");
        entity.Property(e => e.Name).HasColumnName("NAME");
        entity.Property(e => e.NormalizedName).HasColumnName("NORMALIZED_NAME");

        entity.Property(e => e.DateCreated).HasColumnName("DATE_CREATED").HasColumnType("timestamptz");
        entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
    }
}
