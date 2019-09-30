using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Data.Mappings
{
    public class MassCommunicationStatusMap
        : IEntityTypeConfiguration<MassCommunicationStatus>
    {
        public void Configure(EntityTypeBuilder<MassCommunicationStatus> builder)
        {
            var b = builder;

            b.ToTable("MassCommunicationStatus", "dbo");

            b.HasKey(x => x.MassCommunicationStatusId)
                .HasName("PK_dbo.MassCommunicationStatus_MassCommunicationStatusId");

            b.Property(x => x.MassCommunicationStatusId)
                .ValueGeneratedNever();

            b.Property(x => x.Name)
                .HasMaxLength(50)
                .IsRequired();

            b.Property(x => x.Description)
                .HasMaxLength(255)
                .IsRequired();

            b.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();
        }
    }
}