using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProMassSpammer.Data.Entities;

//using System.ComponentModel.DataAnnotations;

namespace ProMassSpammer.Data.Mappings
{
    public class DeliveryMethodMap
        : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            var b = builder;

            b.ToTable("DeliveryMethod", "dbo");

            b.HasKey(x => x.DeliveryMethodId)
                .HasName("PK_dbo.DeliveryMethod_DeliveryMethodId");

            b.Property(x => x.DeliveryMethodId)
                .ValueGeneratedNever();

            b.Property(e => e.Name)
                .IsUnicode(false)
                .HasMaxLength(100)
                .IsRequired();

            b.Property(e => e.Description)
                .IsUnicode(false)
                .HasMaxLength(500)
                .IsRequired();

            //TODO: Specify default constraint name
            //https://github.com/aspnet/EntityFrameworkCore/issues/11502
            b.Property(e => e.IsActive)
                .HasColumnType("bit")
                .HasDefaultValueSql("1")
                .IsRequired();

            b.Property(e => e.CreatedOnUtc)
                .HasColumnType("datetime2(0)")
                .HasDefaultValueSql("getutcdate()")
                .IsRequired();

            b.Ignore(x => x.DeliveryMethodType);
        }
    }
}