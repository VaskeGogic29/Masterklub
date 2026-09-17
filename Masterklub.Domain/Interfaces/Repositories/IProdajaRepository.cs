using Masterklub.Domain.Entities;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface IProdajaRepository : IRepository<Prodaja>
{
    Task<IEnumerable<(Proizvod Proizvod, int UkupnoProdatihKomada)>> GetTop5NajprodavanijihProizvodaAsync();
}
