using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record ProdavacResponse(
    int Id,
    string Ime,
    string Prezime,
    string Email,
    TipProdavca TipProdavca,
    int BrojPoena,
    int UkupnoOstvarenihBodova,
    int Nivo,
    StatusEntiteta Status);
