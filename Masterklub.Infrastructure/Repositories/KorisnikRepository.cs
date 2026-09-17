using Masterklub.Domain.Entities;
using Masterklub.Domain.Interfaces.Repositories;
using Masterklub.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Masterklub.Infrastructure.Repositories;

public class KorisnikRepository : IKorisnikRepository
{
    private readonly MasterklubDbContext _context;

    public KorisnikRepository(MasterklubDbContext context)
    {
        _context = context;
    }

    public async Task<Korisnik?> GetByEmailAsync(string email)
    {
        return await _context.Korisnici.FirstOrDefaultAsync(k => k.Email == email);
    }
}
