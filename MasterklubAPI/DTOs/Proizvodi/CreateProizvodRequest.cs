using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Proizvodi;

public record CreateProizvodRequest(
    [property: Required, MaxLength(100)] string Naziv,
    [property: Range(1, int.MaxValue)] int BrojBodova,
    [property: EnumDataType(typeof(KategorijaProizvoda))] KategorijaProizvoda Kategorija);
