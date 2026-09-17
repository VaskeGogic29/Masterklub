using Masterklub.Domain.Enums;

namespace Masterklub.Domain.Entities;

public class Prodavac : Korisnik
{
    public const int MaksimalniNivo = 5;
    public const int BodovaPoNivou = 50;

    public TipProdavca TipProdavca { get; set; }

    public int BrojPoena { get; private set; }

    public int UkupnoOstvarenihBodova { get; private set; }

    public int Nivo { get; private set; } = 1;

    public ICollection<Prodaja> Prodaje { get; set; } = new List<Prodaja>();
    public ICollection<NarudzbinaNagrade> NarudzbineNagrada { get; set; } = new List<NarudzbinaNagrade>();


    //ENKAPSULACIJA PRAVILA ZA DODAVANJE I ODUZIMANJE BODOVA
    public void DodajBodove(int brojBodova)
    {
        if (brojBodova <= 0)
            throw new ArgumentException("Broj bodova mora biti pozitivan broj.", nameof(brojBodova));

        BrojPoena += brojBodova;
        UkupnoOstvarenihBodova += brojBodova;
        AzurirajNivo();
    }

    public void OduzmiBodove(int brojBodova)
    {
        if (brojBodova <= 0)
            throw new ArgumentException("Broj bodova mora biti pozitivan broj.", nameof(brojBodova));

        if (BrojPoena < brojBodova)
            throw new InvalidOperationException("Prodavac nema dovoljno bodova za ovu razmenu.");

        BrojPoena -= brojBodova;
    }

    public bool MozeDaNaruciNagradu(Nagrada nagrada)
    {
        ArgumentNullException.ThrowIfNull(nagrada);

        return Nivo >= nagrada.Nivo && BrojPoena >= nagrada.BrojPoena;
    }

    private void AzurirajNivo()
    {
        var izracunatiNivo = (UkupnoOstvarenihBodova / BodovaPoNivou) + 1;
        Nivo = Math.Min(izracunatiNivo, MaksimalniNivo);
    }
}
