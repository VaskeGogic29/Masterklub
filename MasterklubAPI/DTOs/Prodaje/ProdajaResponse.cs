namespace MasterklubAPI.DTOs.Prodaje;

public record ProdajaResponse(
    int Id,
    int ProdavacId,
    string ProdavacImePrezime,
    int ProizvodId,
    string ProizvodNaziv,
    int Kolicina,
    DateTime DatumProdaje,
    int BrojBodovaOstvarenih);
