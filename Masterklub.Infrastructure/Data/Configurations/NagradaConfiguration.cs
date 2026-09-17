using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class NagradaConfiguration : IEntityTypeConfiguration<Nagrada>
{
    public void Configure(EntityTypeBuilder<Nagrada> builder)
    {
        builder.ToTable("Nagrade", t =>
        {
            t.HasCheckConstraint("CK_Nagrada_BrojPoena", "[BrojPoena] >= 0");
            t.HasCheckConstraint("CK_Nagrada_Nivo", "[Nivo] BETWEEN 1 AND 5");
        });

        builder.HasKey(n => n.Id);

        builder.Property(n => n.Naziv)
            .IsRequired()
            .HasMaxLength(150);

        builder.HasIndex(n => n.Naziv)
            .IsUnique();

        builder.Property(n => n.BrojPoena)
            .IsRequired();

        builder.Property(n => n.Nivo)
            .IsRequired();

        builder.Property(n => n.Status)
            .IsRequired()
            .HasConversion<string>()
            .HasMaxLength(20);
    }
}
