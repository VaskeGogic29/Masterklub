using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Proizvodi;

public record UpdateProizvodRequest(
    [Required, MaxLength(100)] string Naziv,
    [Range(1, int.MaxValue)] int BrojBodova,
    [EnumDataType(typeof(KategorijaProizvoda))] KategorijaProizvoda Kategorija,
    [EnumDataType(typeof(StatusEntiteta))] StatusEntiteta Status);
