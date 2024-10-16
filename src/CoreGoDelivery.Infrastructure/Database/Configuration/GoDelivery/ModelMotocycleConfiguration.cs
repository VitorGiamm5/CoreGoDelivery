using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotocycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    public class ModelMotocycleConfiguration : IEntityTypeConfiguration<ModelMotocycleEntity>
    {
        public void Configure(EntityTypeBuilder<ModelMotocycleEntity> builder)
        {
            builder.ToTable("tb_modelMotocycle");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("ID_MODEL_MOTOCYCLE");
            builder.Property(t => t.Name).HasColumnName("NAME");
            builder.Property(t => t.NormalizedName).HasColumnName("NORMALIZED_NAME");
        }
    }
}
