using Masterklub.Domain.Enums;

namespace Masterklub.Domain.Entities;

public class Nagrada
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public int BrojPoena { get; set; }
    public int Nivo { get; set; }
    public StatusEntiteta Status { get; set; } = StatusEntiteta.Aktivan;

    public ICollection<NarudzbinaNagrade> NarudzbineNagrada { get; set; } = new List<NarudzbinaNagrade>();
}
