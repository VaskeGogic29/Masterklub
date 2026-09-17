using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Repositories;

public class ProdajaRepository : Repository<Prodaja>, IProdajaRepository
{
    public ProdajaRepository(MasterklubDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<(Proizvod Proizvod, int UkupnoProdatihKomada)>> GetTop5NajprodavanijihProizvodaAsync()
    {
        var poredjenje = await Context.Prodaje
            .GroupBy(p => p.ProizvodId)
            .Select(g => new
            {
                ProizvodId = g.Key,
                UkupnoProdatihKomada = g.Sum(p => p.Kolicina)
            })
            .OrderByDescending(x => x.UkupnoProdatihKomada)
            .Take(5)
            .ToListAsync();

        var idjevi = poredjenje.Select(x => x.ProizvodId).ToList();

        var proizvodi = await Context.Proizvodi
            .Where(p => idjevi.Contains(p.Id))
            .ToDictionaryAsync(p => p.Id);

        return poredjenje.Select(x => (proizvodi[x.ProizvodId], x.UkupnoProdatihKomada));
    }
}
