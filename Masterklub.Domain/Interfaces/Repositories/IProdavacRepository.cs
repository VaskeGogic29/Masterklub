using Masterklub.Domain.Entities;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface IProdavacRepository : IRepository<Prodavac>
{
    Task<IEnumerable<Prodavac>> GetTop5Async();
}
