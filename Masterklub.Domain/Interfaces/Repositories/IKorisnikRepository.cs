using Masterklub.Domain.Entities;

namespace Masterklub.Domain.Interfaces.Repositories;

public interface IKorisnikRepository
{
    Task<Korisnik?> GetByEmailAsync(string email);
}
