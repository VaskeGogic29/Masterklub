using System.ComponentModel.DataAnnotations;
using Masterklub.Domain.Enums;

namespace MasterklubAPI.DTOs.Prodavci;

public record AdminUpdateProdavacRequest(
    [EnumDataType(typeof(TipProdavca))] TipProdavca TipProdavca,
    [EnumDataType(typeof(StatusEntiteta))] StatusEntiteta Status);
