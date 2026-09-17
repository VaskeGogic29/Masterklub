using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;

namespace Masterklub.Infrastructure.Repositories;

public class AdministratorRepository : Repository<Administrator>, IAdministratorRepository
{
    public AdministratorRepository(MasterklubDbContext context) : base(context)
    {
    }
}
