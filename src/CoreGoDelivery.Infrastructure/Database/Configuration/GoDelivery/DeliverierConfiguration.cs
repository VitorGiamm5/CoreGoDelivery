using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery
{
    public class DeliverierConfiguration : IEntityTypeConfiguration<DeliverierEntity>
    {
        public void Configure(EntityTypeBuilder<DeliverierEntity> builder)
        {
            builder.ToTable("tb_deliverier");
            builder.HasKey(t => t.DeliverierId);

            builder.Property(t => t.DeliverierId).HasColumnName("ID_DELIVERIER");
            builder.Property(t => t.FullName).HasColumnName("FULL_NAME");
            builder.Property(t => t.CNPJ).HasColumnName("CNPJ");
            builder.Property(t => t.BirthDate).HasColumnName("DATE_BIRTH");

            builder.Property(t => t.LicenseNumberId).HasColumnName("ID_FK_LICENSE_NUMBER");
        }
    }
}
