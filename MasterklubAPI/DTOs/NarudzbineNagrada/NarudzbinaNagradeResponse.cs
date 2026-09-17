namespace MasterklubAPI.DTOs.NarudzbineNagrada;

public record NarudzbinaNagradeResponse(
    int Id,
    int ProdavacId,
    string ProdavacImePrezime,
    int NagradaId,
    string NagradaNaziv,
    DateTime DatumNarudzbine,
    int BrojPoenaOduzetih);
