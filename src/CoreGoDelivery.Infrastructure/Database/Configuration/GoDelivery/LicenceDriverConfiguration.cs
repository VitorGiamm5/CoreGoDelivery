using CoreGoDelivery.Domain.Entities.GoDelivery.LicenceDriver;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CoreGoDelivery.Infrastructure.Database.Configuration.GoDelivery;

public class LicenceDriverConfiguration : IEntityTypeConfiguration<LicenceDriverEntity>
{
    public void Configure(EntityTypeBuilder<LicenceDriverEntity> entity)
    {
        entity.ToTable("tb_licenceDriver");
        entity.HasKey(e => e.Id);

        entity.Property(e => e.Id).HasColumnName("ID_LICENSE_DRIVER");
        entity.Property(e => e.IssueDate).HasColumnName("ISSUE_DATE").HasColumnType("timestamptz");
        entity.Property(e => e.ExpiryDate).HasColumnName("EXPIRY_DATE").HasColumnType("timestamptz");
        entity.Property(e => e.ImageUrlReference).HasColumnName("IMAGE_URL_REFERENCE");
        entity.Property(e => e.DateUpdated).HasColumnName("DATE_UPDATE").HasColumnType("timestamptz").IsRequired(false);

        entity.Property(e => e.DateCreated).HasColumnName("DATE_CREATED").HasColumnType("timestamptz");
        entity.Property(e => e.CreatedBy).HasColumnName("CREATED_BY");
    }
}
