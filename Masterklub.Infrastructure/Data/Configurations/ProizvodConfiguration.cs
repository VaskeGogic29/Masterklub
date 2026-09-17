using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class ProizvodConfiguration : IEntityTypeConfiguration<Proizvod>
{
    public void Configure(EntityTypeBuilder<Proizvod> builder)
    {
        builder.ToTable("Proizvodi", t =>
        {
            t.HasCheckConstraint("CK_Proizvod_BrojBodova", "[BrojBodova] >= 0");
        });

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Naziv)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(p => p.Naziv)
            .IsUnique();

        builder.Property(p => p.BrojBodova)
            .IsRequired();

        builder.Property(p => p.Kategorija)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(30);

        builder.Property(p => p.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
