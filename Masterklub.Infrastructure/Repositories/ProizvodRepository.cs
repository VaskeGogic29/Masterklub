using Masterklub.Domain.Entities;
using Masterklub.Domain.Enums;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Repositories;

public class ProizvodRepository : Repository<Proizvod>, IProizvodRepository
{
    public ProizvodRepository(MasterklubDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Proizvod>> GetPoKategorijiAsync(KategorijaProizvoda kategorija)
    {
        return await Context.Proizvodi
            .Where(p => p.Kategorija == kategorija)
            .ToListAsync();
    }
}
