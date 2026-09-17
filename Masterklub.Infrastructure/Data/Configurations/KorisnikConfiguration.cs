using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class KorisnikConfiguration : IEntityTypeConfiguration<Korisnik>
{
    public void Configure(EntityTypeBuilder<Korisnik> builder)
    {
        builder.ToTable("Korisnici");

        builder.HasKey(k => k.Id);

        builder.Property(k => k.Ime)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(k => k.Prezime)
            .IsRequired()
            .HasMaxLength(50);

        builder.Property(k => k.Email)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(k => k.Email)
            .IsUnique();

        builder.Property(k => k.LozinkaHash)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(k => k.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);

        builder.HasDiscriminator<string>("TipKorisnika")
            .HasValue<Administrator>("Administrator")
            .HasValue<Prodavac>("Prodavac");
    }
}
