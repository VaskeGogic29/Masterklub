using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;

namespace Masterklub.Infrastructure.Repositories;

public class NagradaRepository : Repository<Nagrada>, INagradaRepository
{
    public NagradaRepository(MasterklubDbContext context) : base(context)
    {
    }
}
