using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Data.Mappings
{
    public class RecipientMap
        : IEntityTypeConfiguration<Recipient>
    {
        public void Configure(EntityTypeBuilder<Recipient> builder)
        {
            var b = builder;

            b.ToTable("Recipient", "dbo");

            b.HasKey(x => x.RecipientId)
                .HasName("PK_dbo.Recipient_RecipientId");

            b.Property(x => x.MassCommunicationId)
                .IsRequired();

            b.Property(x => x.TransmissionStatusId)
                .IsRequired();

            b.Ignore(x => x.TransmissionStatus);

            b.Property(x => x.TransmissionStatusMessage)
                .IsUnicode(false)
                .HasMaxLength(1000)
                .IsRequired(false);

            b.Property(x => x.ContactString)
                .IsUnicode(false)
                .HasMaxLength(1000)
                .IsRequired();

            b.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();

            b.Property(e => e.ModifiedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()");

            //1 RecipientId -> 1 MassCommunicationId
            //This is where the FK exists, so it is declared here
            b.HasOne(x => x.MassCommunication)
                .WithMany(x => x.Recipients)
                .HasForeignKey(x => x.MassCommunicationId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Recipient_dbo.MassCommunication_MassCommunicationId");

            b.HasIndex(x => x.MassCommunicationId)
                .HasName("IX_dbo.Recipient_MassCommunicationId")
                .IsUnique(false);

            //1 RecipientId -> 1 TransmissionStatusId
            b.HasOne(x => x.TransmissionStatusEntity)
                .WithOne()
                .HasForeignKey<Recipient>(x => x.TransmissionStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.Recipient_dbo.TransmissionStatus_TransmissionStatusId");

            b.HasIndex(x => x.TransmissionStatusId)
                .HasName("IX_dbo.Recipient_TransmissionStatusId")
                .IsUnique(false);
        }
    }
}