namespace Masterklub.Domain.Entities;

public class NarudzbinaNagrade
{
    public int Id { get; private set; }

    public int ProdavacId { get; private set; }
    public Prodavac Prodavac { get; private set; } = null!;

    public int NagradaId { get; private set; }
    public Nagrada Nagrada { get; private set; } = null!;

    public DateTime DatumNarudzbine { get; private set; }
    public int BrojPoenaOduzetih { get; private set; }

    private NarudzbinaNagrade()
    {
    }

    public NarudzbinaNagrade(Prodavac prodavac, Nagrada nagrada)
    {
        ArgumentNullException.ThrowIfNull(prodavac);
        ArgumentNullException.ThrowIfNull(nagrada);

        if (!prodavac.MozeDaNaruciNagradu(nagrada))
            throw new InvalidOperationException(
                "Prodavac ne ispunjava uslove (nivo ili broj bodova) za ovu nagradu.");

        Prodavac = prodavac;
        ProdavacId = prodavac.Id;
        Nagrada = nagrada;
        NagradaId = nagrada.Id;
        DatumNarudzbine = DateTime.UtcNow;
        BrojPoenaOduzetih = nagrada.BrojPoena;

        prodavac.OduzmiBodove(BrojPoenaOduzetih);
    }
}
