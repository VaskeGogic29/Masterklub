using Masterklub.Domain.Entities;

namespace MasterklubAPI.Auth;

public interface IJwtTokenGenerator
{
    string GenerisiToken(Korisnik korisnik, string uloga);
}
