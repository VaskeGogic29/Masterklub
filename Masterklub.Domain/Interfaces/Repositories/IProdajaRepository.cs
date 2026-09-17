using Masterklub.Domain.Entities;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface IProdajaRepository : IRepository<Prodaja>
{
    Task<IEnumerable<Prodaja>> GetZaProdavcaAsync(int prodavacId);

    Task<IEnumerable<(Proizvod Proizvod, int UkupnoProdatihKomada)>> GetTop5NajprodavanijihProizvodaAsync();
}
