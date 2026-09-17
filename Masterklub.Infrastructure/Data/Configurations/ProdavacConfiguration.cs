using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
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

        builder.HasData(
            new
            {
                Id = 2,
                Ime = "Marko",
                Prezime = "Marković",
                Email = "marko@masterklub.rs",
                LozinkaHash = "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==",
                Status = StatusEntiteta.Aktivan,
                TipProdavca = TipProdavca.Prodavac,
                BrojPoena = 0,
                UkupnoOstvarenihBodova = 0,
                Nivo = 1
            },
            new
            {
                Id = 3,
                Ime = "Ana",
                Prezime = "Anić",
                Email = "ana@masterklub.rs",
                LozinkaHash = "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==",
                Status = StatusEntiteta.Aktivan,
                TipProdavca = TipProdavca.Prodavac,
                BrojPoena = 0,
                UkupnoOstvarenihBodova = 0,
                Nivo = 1
            },
            new
            {
                Id = 4,
                Ime = "Jovan",
                Prezime = "Jovanović",
                Email = "jovan@masterklub.rs",
                LozinkaHash = "AQAAAAEAAYagAAAAEGjTLdohWuB5XfYIxllgLOOj8hB1KUCE6JuKKbi5G3KlunRZs428WUUYiIiAeYawrA==",
                Status = StatusEntiteta.Aktivan,
                TipProdavca = TipProdavca.Menadzer,
                BrojPoena = 0,
                UkupnoOstvarenihBodova = 0,
                Nivo = 1
            });
    }
}
