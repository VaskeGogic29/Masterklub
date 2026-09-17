using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record AdminUpdateProdavacRequest(
    TipProdavca TipProdavca,
    StatusEntiteta Status);
