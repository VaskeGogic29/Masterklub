using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Proizvodi;

public record CreateProizvodRequest(string Naziv, int BrojBodova, KategorijaProizvoda Kategorija);
