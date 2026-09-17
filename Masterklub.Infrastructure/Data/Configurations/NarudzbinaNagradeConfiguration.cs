using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Masterklub.Infrastructure.Data.Configurations;

public class NarudzbinaNagradeConfiguration : IEntityTypeConfiguration<NarudzbinaNagrade>
{
    public void Configure(EntityTypeBuilder<NarudzbinaNagrade> builder)
    {
        builder.ToTable("NarudzbineNagrada", t =>
        {
            t.HasCheckConstraint("CK_NarudzbinaNagrade_BrojPoenaOduzetih", "[BrojPoenaOduzetih] >= 0");
        });

        builder.HasKey(n => n.Id);

        builder.Property(n => n.DatumNarudzbine)
            .IsRequired();

        builder.Property(n => n.BrojPoenaOduzetih)
            .IsRequired();

        builder.HasOne(n => n.Prodavac)
            .WithMany(p => p.NarudzbineNagrada)
            .HasForeignKey(n => n.ProdavacId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(n => n.Nagrada)
            .WithMany(nn => nn.NarudzbineNagrada)
            .HasForeignKey(n => n.NagradaId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}
