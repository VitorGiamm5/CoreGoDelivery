using CoreGoDelivery.Domain.Entities.GoDelivery.ModelMotorcycle;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    public class ModelMotocycleConfiguration : IEntityTypeConfiguration<ModelMotorcycleEntity>
    {
        public void Configure(EntityTypeBuilder<ModelMotorcycleEntity> builder)
        {
            builder.ToTable("tb_modelMotorcycle");
            builder.HasKey(t => t.Id);

            builder.Property(t => t.Id).HasColumnName("ID_MODEL_MOTORCYCLE");
            builder.Property(t => t.Name).HasColumnName("NAME");
            builder.Property(t => t.NormalizedName).HasColumnName("NORMALIZED_NAME");
        }
    }
}
