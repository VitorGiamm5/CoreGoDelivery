using CoreGoDelivery.Domain.Entities.GoDelivery.Deliverier;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class DeliverierConfiguration : IEntityTypeConfiguration<DeliverierEntity>
{
    public void Configure(EntityTypeBuilder<DeliverierEntity> builder)
    {
        builder.ToTable("tb_deliverier");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_DELIVERIER");
        builder.Property(t => t.FullName).HasColumnName("FULL_NAME");
        builder.Property(t => t.Cnpj).HasColumnName("CNPJ");
        builder.Property(t => t.BirthDate).HasColumnName("DATE_BIRTH");
        builder.Property(t => t.LicenceDriverId).HasColumnName("ID_FK_LICENSE_DRIVER");
    }
}
