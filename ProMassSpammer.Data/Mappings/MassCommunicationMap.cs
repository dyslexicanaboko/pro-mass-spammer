using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMassSpammer.Data.Entities;

namespace ProMassSpammer.Data.Mappings
{
    public class MassCommunicationMap
        : IEntityTypeConfiguration<MassCommunication>
    {
        public void Configure(EntityTypeBuilder<MassCommunication> builder)
        {
            var b = builder;

            b.ToTable("MassCommunication", "dbo");

            b.HasKey(x => x.MassCommunicationId)
                .HasName("PK_dbo.MassCommunication_MassCommunicationId");

            b.Property(x => x.Title)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(x => x.MassCommunicationStatusId)
                .IsRequired();

            b.Ignore(x => x.MassCommunicationStatus);

            b.Property(x => x.DeliveryMethodId)
                .IsRequired();

            b.Ignore(x => x.DeliveryMethod);
            
            b.Property(x => x.StatusMessage)
                .IsUnicode(false)
                .HasMaxLength(1000)
                .IsRequired(false);

            b.Property(x => x.Catalyst)
                .IsUnicode(false)
                .HasMaxLength(1000)
                .IsRequired();

            b.Property(x => x.From)
                .IsUnicode(false)
                .HasMaxLength(1000)
                .IsRequired();

            b.Property(x => x.Subject)
                .IsUnicode(false)
                .HasMaxLength(78) //RFC 2822
                .IsRequired();

            b.Property(x => x.Body)
                .IsUnicode()
                .IsRequired();

            b.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();

            b.Property(e => e.ModifiedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()");

            //1 MassCommunicationId -> * RecipientId
            //Don't specify the key name here because this is not where the FK exists
            //This is only for the navigation property definition
            b.HasMany(x => x.Recipients)
                .WithOne(x => x.MassCommunication)
                .HasForeignKey(x => x.MassCommunicationId)
                .IsRequired();

            //1 MassCommunicationId -> 1 DeliveryMethodId
            b.HasOne(x => x.DeliveryMethodEntity)
                .WithOne()
                .HasForeignKey<MassCommunication>(x => x.DeliveryMethodId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.MassCommunication_dbo.DeliveryMethod_DeliveryMethodId");

            b.HasIndex(x => x.DeliveryMethodId)
                .HasName("IX_dbo.MassCommunication_DeliveryMethodId")
                .IsUnique(false);

            //1 MassCommunicationId -> 1 MassCommunicationStatusId
            b.HasOne(x => x.MassCommunicationStatusEntity)
                .WithOne()
                .HasForeignKey<MassCommunication>(x => x.MassCommunicationStatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("FK_dbo.MassCommunication_dbo.MassCommunicationStatus_MassCommunicationStatusId");

            b.HasIndex(x => x.MassCommunicationStatusId)
                .HasName("IX_dbo.MassCommunication_MassCommunicationStatusId")
                .IsUnique(false);
        }
    }
}