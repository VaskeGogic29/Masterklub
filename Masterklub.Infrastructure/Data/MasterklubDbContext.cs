using Masterklub.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Data;

public class MasterklubDbContext : DbContext
{
    public MasterklubDbContext(DbContextOptions<MasterklubDbContext> options)
        : base(options)
    {
    }

    public DbSet<Administrator> Administratori => Set<Administrator>();
    public DbSet<Prodavac> Prodavci => Set<Prodavac>();
    public DbSet<Proizvod> Proizvodi => Set<Proizvod>();
    public DbSet<Nagrada> Nagrade => Set<Nagrada>();
    public DbSet<Prodaja> Prodaje => Set<Prodaja>();
    public DbSet<NarudzbinaNagrade> NarudzbineNagrada => Set<NarudzbinaNagrade>();
    public DbSet<Korisnik> Korisnici => Set<Korisnik>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(MasterklubDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}
