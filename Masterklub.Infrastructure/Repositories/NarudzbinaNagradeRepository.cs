using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;

namespace Masterklub.Infrastructure.Repositories;

public class NarudzbinaNagradeRepository : Repository<NarudzbinaNagrade>, INarudzbinaNagradeRepository
{
    public NarudzbinaNagradeRepository(MasterklubDbContext context) : base(context)
    {
    }
}
