using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class ProdajaConfiguration : IEntityTypeConfiguration<Prodaja>
{
    public void Configure(EntityTypeBuilder<Prodaja> builder)
    {
        builder.ToTable("Prodaje", t =>
        {
            t.HasCheckConstraint("CK_Prodaja_Kolicina", "[Kolicina] > 0");
            t.HasCheckConstraint("CK_Prodaja_BrojBodovaOstvarenih", "[BrojBodovaOstvarenih] >= 0");
        });

        builder.HasKey(p => p.Id);

        builder.Property(p => p.Kolicina)
            .IsRequired();

        builder.Property(p => p.DatumProdaje)
            .IsRequired();

        builder.Property(p => p.BrojBodovaOstvarenih)
            .IsRequired();

        builder.HasOne(p => p.Prodavac)
            .WithMany(pr => pr.Prodaje)
            .HasForeignKey(p => p.ProdavacId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(p => p.Proizvod)
            .WithMany(pr => pr.Prodaje)
            .HasForeignKey(p => p.ProizvodId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
