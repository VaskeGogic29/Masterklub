using System.Security.Claims;

namespace MasterklubAPI.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static int GetKorisnikId(this ClaimsPrincipal korisnik)
    {
        var vrednost = korisnik.FindFirstValue(ClaimTypes.NameIdentifier)
            ?? throw new InvalidOperationException("JWT token ne sadrži UserId claim.");

        return int.Parse(vrednost);
    }
}
