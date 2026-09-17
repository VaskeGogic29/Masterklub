using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Proizvodi;

public record ProizvodResponse(
    int Id,
    string Naziv,
    int BrojBodova,
    KategorijaProizvoda Kategorija,
    StatusEntiteta Status);
