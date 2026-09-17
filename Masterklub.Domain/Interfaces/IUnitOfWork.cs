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

    Task<int> SaveChangesAsync();
}
