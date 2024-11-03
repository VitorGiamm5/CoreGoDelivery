using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class LicenceDriverConfiguration : IEntityTypeConfiguration<LicenceDriverEntity>
{
    public void Configure(EntityTypeBuilder<LicenceDriverEntity> builder)
    {
        builder.ToTable("tb_licenceDriver");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Id).HasColumnName("ID_LICENSE_DRIVER");
        builder.Property(t => t.Type).HasColumnName("ID_LICENSE_TYPE");
        builder.Property(t => t.ImageUrlReference).HasColumnName("IMAGE_URL_REFERENCE");
    }
}
