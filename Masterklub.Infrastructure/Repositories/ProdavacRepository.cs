using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Repositories;

public class ProdavacRepository : Repository<Prodavac>, IProdavacRepository
{
    public ProdavacRepository(MasterklubDbContext context) : base(context)
    {
    }

    public async Task<IEnumerable<Prodavac>> GetTop5Async()
    {
        return await Context.Prodavci
            .OrderByDescending(p => p.UkupnoOstvarenihBodova)
            .Take(5)
            .ToListAsync();
    }
}
