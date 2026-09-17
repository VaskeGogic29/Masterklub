using Masterklub.Domain.Enums;

namespace Masterklub.Domain.Entities;

public abstract class Korisnik
{
    public int Id { get; set; }
    public string Ime { get; set; } = string.Empty;
    public string Prezime { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public StatusEntiteta Status { get; set; } = StatusEntiteta.Aktivan;
}
