using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Repositories;

public class NarudzbinaNagradeRepository : Repository<NarudzbinaNagrade>, INarudzbinaNagradeRepository
{
    public NarudzbinaNagradeRepository(MasterklubDbContext context) : base(context)
    {
    }

    public override async Task<NarudzbinaNagrade?> GetByIdAsync(int id)
    {
        return await Context.NarudzbineNagrada
            .Include(n => n.Prodavac)
            .Include(n => n.Nagrada)
            .FirstOrDefaultAsync(n => n.Id == id);
    }

    public override async Task<IEnumerable<NarudzbinaNagrade>> GetAllAsync()
    {
        return await Context.NarudzbineNagrada
            .Include(n => n.Prodavac)
            .Include(n => n.Nagrada)
            .ToListAsync();
    }

    public async Task<IEnumerable<NarudzbinaNagrade>> GetZaProdavcaAsync(int prodavacId)
    {
        return await Context.NarudzbineNagrada
            .Include(n => n.Prodavac)
            .Include(n => n.Nagrada)
            .Where(n => n.ProdavacId == prodavacId)
            .ToListAsync();
    }
}
