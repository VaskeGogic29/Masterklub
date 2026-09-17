namespace Masterklub.Domain.Entities;

public class Prodaja
{
    public int Id { get; private set; }

    public int ProdavacId { get; private set; }
    public Prodavac Prodavac { get; private set; } = null!;

    public int ProizvodId { get; private set; }
    public Proizvod Proizvod { get; private set; } = null!;

    public int Kolicina { get; private set; }
    public DateTime DatumProdaje { get; private set; }
    public int BrojBodovaOstvarenih { get; private set; }

    private Prodaja()
    {
    }

    public Prodaja(Prodavac prodavac, Proizvod proizvod, int kolicina)
    {
        ArgumentNullException.ThrowIfNull(prodavac);
        ArgumentNullException.ThrowIfNull(proizvod);

        if (kolicina <= 0)
            throw new ArgumentException("Količina mora biti veća od nule.", nameof(kolicina));

        Prodavac = prodavac;
        ProdavacId = prodavac.Id;
        Proizvod = proizvod;
        ProizvodId = proizvod.Id;
        Kolicina = kolicina;
        DatumProdaje = DateTime.UtcNow;
        BrojBodovaOstvarenih = proizvod.BrojBodova * kolicina;

        prodavac.DodajBodove(BrojBodovaOstvarenih);
    }
}
