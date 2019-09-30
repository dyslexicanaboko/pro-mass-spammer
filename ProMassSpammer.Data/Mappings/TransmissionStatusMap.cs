using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Data.Mappings
{
    public class TransmissionStatusMap
        : IEntityTypeConfiguration<TransmissionStatus>
    {
        public void Configure(EntityTypeBuilder<TransmissionStatus> builder)
        {
            var b = builder;

            b.ToTable("TransmissionStatus", "dbo");

            b.HasKey(x => x.TransmissionStatusId)
                .HasName("PK_dbo.TransmissionStatus_TransmissionStatusId");

            b.Property(x => x.TransmissionStatusId)
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