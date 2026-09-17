using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record AdminUpdateProdavacRequest(
    [property: EnumDataType(typeof(TipProdavca))] TipProdavca TipProdavca,
    [property: EnumDataType(typeof(StatusEntiteta))] StatusEntiteta Status);
