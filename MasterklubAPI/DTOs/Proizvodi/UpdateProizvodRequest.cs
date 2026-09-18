using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Proizvodi;

public record UpdateProizvodRequest(string Naziv, int BrojBodova, KategorijaProizvoda Kategorija, StatusEntiteta Status);
