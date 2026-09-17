using Masterklub.Domain.Interfaces.Repositories;

namespace Masterklub.Domain.Interfaces;

public interface IUnitOfWork : IDisposable
{
    IAdministratorRepository Administratori { get; }
    IProdavacRepository Prodavci { get; }
    IProizvodRepository Proizvodi { get; }
    INagradaRepository Nagrade { get; }
    IProdajaRepository Prodaje { get; }
    INarudzbinaNagradeRepository NarudzbineNagrada { get; }
    IKorisnikRepository Korisnici { get; }

    Task<int> SaveChangesAsync();
}
