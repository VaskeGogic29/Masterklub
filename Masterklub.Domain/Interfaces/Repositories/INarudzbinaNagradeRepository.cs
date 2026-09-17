using Masterklub.Domain.Entities;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface INarudzbinaNagradeRepository : IRepository<NarudzbinaNagrade>
{
    Task<IEnumerable<NarudzbinaNagrade>> GetZaProdavcaAsync(int prodavacId);
}
