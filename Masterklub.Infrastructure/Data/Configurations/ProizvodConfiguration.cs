using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
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

        builder.HasData(
            new Proizvod { Id = 1, Naziv = "Televizor Samsung 43\"", BrojBodova = 30, Kategorija = KategorijaProizvoda.TV, Status = StatusEntiteta.Aktivan },
            new Proizvod { Id = 2, Naziv = "Televizor LG 55\"", BrojBodova = 50, Kategorija = KategorijaProizvoda.TV, Status = StatusEntiteta.Aktivan },
            new Proizvod { Id = 3, Naziv = "Usisivač Bosch", BrojBodova = 15, Kategorija = KategorijaProizvoda.AparatiZaKucu, Status = StatusEntiteta.Aktivan },
            new Proizvod { Id = 4, Naziv = "Veš mašina Gorenje", BrojBodova = 40, Kategorija = KategorijaProizvoda.AparatiZaKucu, Status = StatusEntiteta.Aktivan },
            new Proizvod { Id = 5, Naziv = "Klima Midea 12000 BTU", BrojBodova = 25, Kategorija = KategorijaProizvoda.Klime, Status = StatusEntiteta.Aktivan },
            new Proizvod { Id = 6, Naziv = "Klima Daikin 18000 BTU", BrojBodova = 45, Kategorija = KategorijaProizvoda.Klime, Status = StatusEntiteta.Aktivan });
    }
}
