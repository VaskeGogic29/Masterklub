using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface IProizvodRepository : IRepository<Proizvod>
{
    Task<IEnumerable<Proizvod>> GetPoKategorijiAsync(KategorijaProizvoda kategorija);
}
