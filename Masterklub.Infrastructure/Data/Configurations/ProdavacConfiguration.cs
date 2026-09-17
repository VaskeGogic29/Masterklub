using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class ProdavacConfiguration : IEntityTypeConfiguration<Prodavac>
{
    public void Configure(EntityTypeBuilder<Prodavac> builder)
    {
        builder.ToTable(t =>
        {
            t.HasCheckConstraint("CK_Prodavac_Nivo", "[Nivo] BETWEEN 1 AND 5");
            t.HasCheckConstraint("CK_Prodavac_BrojPoena", "[BrojPoena] >= 0");
            t.HasCheckConstraint("CK_Prodavac_UkupnoOstvarenihBodova", "[UkupnoOstvarenihBodova] >= 0");
        });

        builder.Property(p => p.TipProdavca)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.Property(p => p.BrojPoena)
            .IsRequired();

        builder.Property(p => p.UkupnoOstvarenihBodova)
            .IsRequired();

        builder.Property(p => p.Nivo)
            .IsRequired();
    }
}
