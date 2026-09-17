using Masterklub.Domain.Enums;

namespace Masterklub.Domain.Entities;

public class Proizvod
{
    public int Id { get; set; }
    public string Naziv { get; set; } = string.Empty;
    public int BrojBodova { get; set; }
    public KategorijaProizvoda Kategorija { get; set; }
    public StatusEntiteta Status { get; set; } = StatusEntiteta.Aktivan;

    public ICollection<Prodaja> Prodaje { get; set; } = new List<Prodaja>();
}
